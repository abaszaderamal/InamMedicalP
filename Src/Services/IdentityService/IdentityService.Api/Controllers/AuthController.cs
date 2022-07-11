using System.Security.Claims;
using AutoMapper;
using IdentityService.Api.DAL;
using IdentityService.Api.Handlers;
using IdentityService.Api.Models;
using Med.Shared.ControllerBases;
using Med.Shared.Dtos;
using Med.Shared.Dtos.User;
using Med.Shared.Entities;
using Med.Shared.Helpers;
using Med.Shared.HttpStatusCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : CMControllerBase
    {
        #region Ctor

        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AuthController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            AppDbContext context,
            IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
        }

        #endregion


        #region Ok

        [HttpGet("Ok")]
        public IActionResult Ok()
        {
            return Ok("Okeydir Cavid");
        }

        #endregion

        #region RefreshToken

        [HttpPost("RefreshTokenLogin")]
        //[Authorize]
        public async Task<Response<Token>> RefreshTokenLogin([FromBody] string refreshToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);

            //User user = await _context.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.Now)
            {
                TokenHandler tokenHandler = new TokenHandler(_configuration);
                var userRoles = await _userManager.GetRolesAsync(user);
                Token token = tokenHandler.CreateAccessToken(user, userRoles);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenEndDate = token.Expiration.AddMinutes(3);
                await _context.SaveChangesAsync();

                return Response<Token>.Success(token, StatusCodes.Status200OK);
            }

            return Response<Token>.Fail("Ref Token Tapilmadi", StatusCodes.Status400BadRequest);
        }

        #endregion

        //logged user info need

        #region GEtUserINfo

        [HttpGet]
        [Authorize]
        public async Task<Response<UserInfoDto>> GetUserInfo()
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AppUser loggedUser =
                await _userManager.Users.Where(p => p.Id == userId).AsNoTracking().FirstOrDefaultAsync();
            if (loggedUser is null)
                return Response<UserInfoDto>.Fail("Not Found", StatusCodes.Status404NotFound);

            string Role = (await _userManager.GetRolesAsync(loggedUser)).FirstOrDefault();

            UserInfoDto userInfo = _mapper.Map<UserInfoDto>(loggedUser);

            userInfo.Role = CheckRole(Role);


            return Response<UserInfoDto>.Success(userInfo, StatusCodes.Status404NotFound);
        }

        #endregion


        #region GEtUserINfoMore

        [HttpGet("MoreInfo")]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]
        public async Task<Response<UserMoreInfoDto>> GetUserInfoMore()
        {
            //
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AppUser loggedUser =
                await _userManager.Users.Where(p => p.Id == userId).AsNoTracking().FirstOrDefaultAsync();
            if (loggedUser is null)
                return Response<UserMoreInfoDto>.Fail("Not Found", StatusCodes.Status404NotFound);

            string Role = (await _userManager.GetRolesAsync(loggedUser)).FirstOrDefault();

            UserInfoDto userInfo = _mapper.Map<UserInfoDto>(loggedUser);

            userInfo.Role = CheckRole(Role);


            UserMoreInfoDto res = new UserMoreInfoDto()
            {
                UserInfo = userInfo,
                GroupMngrAvg = await GetGMUserAvg(userId),
                ProjectMngrAvg = await GetPMUserAvg(userId),
                Medicines = GetMedicines(userId)
            };


            return Response<UserMoreInfoDto>.Success(res, StatusCodes.Status404NotFound);
        }

        #endregion

        private async Task<int> GetGMUserAvg(string UserId)
        {
 
            var evos = await _context
                .Evaluations
                .Where(p => p.OwnerUserId == UserId
                            &&
                            p.VoterRole == "GroupManager"
                            &&
                            p.CreatedAt.Date.ToString().Substring(0, 8) ==
                            DateTime.UtcNow.Date.ToString().Substring(0, 8))
                .Include(p => p.EvaluationRatings)
                .ToListAsync();

            List<int> values = new List<int>();

            foreach (var evo in evos)
            {
                foreach (var rating in evo.EvaluationRatings)
                {
                    values.Add(rating.Value);
                }
            }

            int result = 0;
            foreach (var val in values)
            {
                result += val;
            }

            try
            {
                result = result / values.Count;
            }
            catch
            {
            }


            return result;
        }

        private async Task<int> GetPMUserAvg(string UserId)
        {
        
            var evos = await _context
                .Evaluations
                .Where(p => p.OwnerUserId == UserId
                            &&
                            p.VoterRole == "ProjectManager"
                            &&
                            p.CreatedAt.Date.ToString().Substring(0, 8) ==
                            DateTime.UtcNow.Date.ToString().Substring(0, 8))
                .Include(p => p.EvaluationRatings)
                .ToListAsync();

            List<int> values = new List<int>();

            foreach (var evo in evos)
            {
                foreach (var rating in evo.EvaluationRatings)
                {
                    values.Add(rating.Value);
                }
            }

            int result = 0;
            foreach (var val in values)
            {
                result += val;
            }

            try
            {
                result = result / values.Count;
            }
            catch
            {
            }


            return result;
        }

        private object GetMedicines(string userId)
        {
            var medicines = _context
                .UserMedicines
                .Where(p => p.AppUserId == userId)
                .Include(p => p.Medicine)
                .Select(p => new
                {
                    Name = p.Medicine.Name,
                }).ToList();
            return medicines;
        }


        #region Revoke Token

        [HttpPost("RevokeRefreshToken")]
        //[Authorize]
        public async Task<Response<Token>> RevokeRefreshToken([FromBody] string refreshToken)
        {
            var user = await _userManager.Users.Where(x => x.RefreshToken == refreshToken).AsNoTracking()
                .FirstOrDefaultAsync();

            //User user = await _context.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
            if (user is null)
            {
                return Response<Token>.Fail("User Tapilmadi", StatusCodes.Status404NotFound);
            }

            user.RefreshToken = null;
            user.RefreshTokenEndDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return Response<Token>.Success(StatusCodes.Status200OK);
        }

        #endregion


        #region Login

        [HttpPost("login")]
        public async Task<ActionResult<Response<Token>>> Login([FromBody] UserLoginDto userLogin)
        {
            if (!ModelState.IsValid)
                return Response<Token>.Fail("Validation Problem", CStatusCodes.Status1017ValidationProblem);

            //await CreateRoll();
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userLogin.Email);
                if (user == null)
                {
                    return Response<Token>.Fail("Email tapılmadı", CStatusCodes.Status1015EmailNotFound);
                }

                if (await _userManager.CheckPasswordAsync(user, userLogin.Password) == false)
                {
                    return Response<Token>.Fail("Etibarsız parol", CStatusCodes.Status1016WrongPassword);
                }

                var result =
                    await _signInManager.PasswordSignInAsync(user.UserName, userLogin.Password,
                        false, /*userLogin.RememberMe,*/
                        true);
                if (result.Succeeded)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    TokenHandler tokenHandler = new TokenHandler(_configuration);
                    Token token = tokenHandler.CreateAccessToken(user, userRoles);


                    token.Role = CheckRole(userRoles.FirstOrDefault());


                    user.RefreshToken = token.RefreshToken;
                    user.RefreshTokenEndDate = userLogin.StayLogged
                        ? token.Expiration.AddMinutes(Convert.ToDouble(_configuration["Token:StayLoggedRefTokenExp"]))
                        : token.Expiration.AddMinutes(Convert.ToDouble(_configuration["Token:RefTokenExp"]));

                    await _context.SaveChangesAsync();


                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        SameSite = SameSiteMode.None,
                        Secure = true
                    };

                    Response.Cookies.Append("X-Access-Token", token.AccessToken, cookieOptions);
                    Response.Cookies.Append("X-Refresh-Token", token.RefreshToken, cookieOptions);
                    //Response.Cookies.Append("X-Access-Token", token.AccessToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                    //Response.Cookies.Append("X-Username", user.UserName, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                    //Response.Cookies.Append("X-Refresh-Token", token.RefreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });


                    return Response<Token>.Success(token, StatusCodes.Status200OK);
                }
                else
                {
                    return Response<Token>.Fail("Fail", StatusCodes.Status400BadRequest);
                }
            }

            List<string> errors = new List<string>();

            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }

            return Response<Token>.Fail(errors, StatusCodes.Status400BadRequest);
        }

        #endregion

        private int CheckRole(string Role)
        {
            int roleStatus = 0;
            switch (Role)
            {
                case "Member":
                    roleStatus = 123;
                    break;
                case "GroupManager":
                    roleStatus = 124;
                    break;
                case "ProjectManager":
                    roleStatus = 125;
                    break;
                case "Admin":
                    roleStatus = 126;
                    break;
                case "SuperAdmin":
                    roleStatus = 127;
                    break;

                default:
                    break;
            }

            return roleStatus;
        }

        #region Register

        [HttpPost("register")]
        public async Task<ActionResult<Response<NoContent>>> Register([FromBody] UserRegisterDto userregDto)
        {
            if (!ModelState.IsValid)
                return Response<NoContent>.Fail("Validation Problem", CStatusCodes.Status1017ValidationProblem);

            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = userregDto.UserName,
                    EmailConfirmed = true,
                    Email = userregDto.EMail,
                    FirstName = userregDto.FirstName,
                    LastName = userregDto.LastName,
                    PhoneNumber = userregDto.PhoneNumber,
                    RefreshToken = String.Empty,
                    ProjectManagerId = userregDto.ProjectManagerId,
                    GroupManagerId = userregDto.GroupManagerId
                };
                bool isExistUsername = _userManager.Users.Any(us => us.UserName == user.UserName);
                if (isExistUsername)
                {
                    return Response<NoContent>.Fail(
                        "Bu İstifadəçi adı artıq mövcuddur. Başqa İstifadəçi adı istifadə edin",
                        StatusCodes.Status400BadRequest);
                }

                bool isExistEmail = _userManager.Users.Any(us => us.Email == user.Email);
                if (isExistEmail)
                {
                    return Response<NoContent>.Fail(
                        "Bu Email artıq mövcuddur. Başqa Email istifadə edin",
                        StatusCodes.Status400BadRequest);
                }

                var result = await _userManager.CreateAsync(user, userregDto.Password);


                if (result.Succeeded)
                {
                    //await _userManager.AddToRoleAsync(user, RoleHelper.UserRoles.Member.ToString());
                    await _userManager.AddToRoleAsync(user, userregDto.Role);

                    return Response<NoContent>.Success(StatusCodes.Status200OK);
                }

                {
                    string errors = String.Empty;
                    if (result.Errors.Count() > 0)
                    {
                        foreach (var error in result.Errors)
                        {
                            errors += error.Description;
                        }
                    }

                    return BadRequest(errors);
                }
            }

            List<string> errorsM = new List<string>();

            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    errorsM.Add(error.ErrorMessage);
                }
            }

            return Response<NoContent>.Fail(errorsM, StatusCodes.Status400BadRequest);
        }

        #endregion


        //#region GetAllUser
        //[HttpGet("Users")]
        //public async Task<List<AppUser>> Users()
        //{
        //    return await _userManager
        //  .Users
        //  .ToListAsync();
        //}
        //#endregion


        #region CreateRoll

        [HttpGet("roll")]
        public async Task<IActionResult> roll()
        {
            await CreateRoll();
            return Ok();
        }

        private async Task CreateRoll()
        {
            foreach (var UserRole in Enum.GetValues(typeof(RoleHelper.UserRoles)))
            {
                if (!await _roleManager.RoleExistsAsync(UserRole.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole {Name = UserRole.ToString()});
                }
            }

            Console.WriteLine("role done");
        }

        #endregion
    }

    public class Product
    {
        public string Name { get; set; }
    }
}