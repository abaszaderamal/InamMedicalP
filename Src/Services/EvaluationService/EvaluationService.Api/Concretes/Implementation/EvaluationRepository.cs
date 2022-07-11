using AutoMapper;
using EvaluationService.Api.Abstracts.Repositories;
using EvaluationService.Api.DAL;
using Med.Shared.Dtos;
using Med.Shared.Dtos.Evaluation;
using Med.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EvaluationService.Api.Concretes.Implementation
{
    public class EvaluationRepository : Repository<Evaluation>, IEvaluationRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public EvaluationRepository(AppDbContext context, UserManager<AppUser> userManager, IMapper mapper) : base(context)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Response<List<EvaluationGetDto>>> GetAllUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles[0];
            List<EvaluationGetDto> result = new();

            if (role == "GroupManager")
            {
                result = await getMembersByGroupMngrIdAsync(user.Id);
            }
            else if (role == "ProjectManager")
            {
                result = await getMemberByProjectMngrIdAsync(user.Id);
            }
            else if (role == "Admin")
            {
                result = await getMembersByAdminIdAsync(user.Id);


            }

            return Response<List<EvaluationGetDto>>.Success(result, StatusCodes.Status200OK);
        }

        public async Task<int> GetUserEvaluationByIdAsync(string userId)
        {
            var result = 0;

            var userevo = await _context
                .Users
                .Where(u => u.Id == userId)
                .Include(p => p.Evaluations)
                .ThenInclude(p => p.EvaluationRatings.Where(p => p.IsDeleted == false))
                .ToListAsync();

            return result;
        }

        public async Task<bool> CheckRaitingStatus(string UserId)
        {
            var t = await _context.Evaluations
                .Where(p => p.AppUserId == UserId)
                .OrderByDescending(p => p.Id)
                .FirstOrDefaultAsync();



            if (t is null) return true;

            //var time1 = t.CreatedAt.Date.ToString().Substring(0, 8);
            //var time2 = DateTime.UtcNow.Date.ToString().Substring(0, 8);
            if (t.CreatedAt.Date.ToString().Substring(0, 8) == DateTime.UtcNow.Date.ToString().Substring(0, 8)) return false;


            return true;


        }

        public async Task  SetAVGValue(string Owner)
        {
            var evos = await _context.Evaluations.Where(p => p.OwnerUserId == Owner && p.CreatedAt.Date.ToString().Substring(0, 8) == DateTime.UtcNow.Date.ToString().Substring(0, 8))
                .Include(p => p.EvaluationRatings).ToListAsync();

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
            result = result / values.Count;

            var user =await _context.Users.Where(p => p.Id == Owner).FirstOrDefaultAsync();
            user.AvgValue=result;
            await _context.SaveChangesAsync();
           

        }


        private async Task<List<EvaluationGetDto>> getMembersByGroupMngrIdAsync(string userId)
        {

            return await _context.Users
               .Where(p => p.IsDeleted == false && p.GroupManagerId == userId)
               .Select(p => new EvaluationGetDto()
               {
                   FirstName = p.FirstName,
                   LastName = p.LastName,
                   UserId = p.Id,

               })
               .AsNoTracking()
               .ToListAsync();



            //return _mapper.Map<List<UserDto>>(await _context
            //    .Users
            //    .Where(p => p.GroupManagerId == userId && p.IsDeleted == false)
            //    .ToListAsync());

        }
        private async Task<List<EvaluationGetDto>> getMembersByAdminIdAsync(string userId)
        {
            //return _mapper.Map<List<UserDto>>(await _context
            //    .Users
            //    .Where(p => p.Id != userId)
            //    .ToListAsync());


            return await _context.Users
                .Where(p => p.IsDeleted == false && p.Id != userId)
                .Select(p => new EvaluationGetDto()
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    UserId = p.Id,

                })
                .AsNoTracking()
                .ToListAsync();


        }

        private async Task<List<EvaluationGetDto>> getMemberByProjectMngrIdAsync(string userId)
        {
            List<AppUser> result = new();


            result.AddRange(await _context
                .Users
                .Where(p => p.ProjectManagerId == userId && p.IsDeleted == false)
                .ToListAsync());
            List<AppUser> resultmember = new();

            foreach (var gropmanager in result)
            {
                resultmember.AddRange(await _userManager.Users.Where(p => p.GroupManagerId == gropmanager.Id && p.IsDeleted == false).ToListAsync());
            }
            result.AddRange(resultmember);


            return result.Select(p => new EvaluationGetDto()
            {
                LastName = p.LastName,
                UserId = p.Id,
                FirstName = p.FirstName

            }).ToList();

        }
    }
}
