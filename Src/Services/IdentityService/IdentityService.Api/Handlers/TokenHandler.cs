using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using IdentityService.Api.Models;
using Med.Shared.Entities;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Api.Handlers
{
    public class TokenHandler
    {
        public IConfiguration Configuration { get; set; }
        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public Models.Token CreateAccessToken(AppUser user, IList<string> userRoles)
        {
            Token tokenInstance = new Token();
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            tokenInstance.Expiration = DateTime.Now
                .AddMinutes(Convert.ToDouble(Configuration["Token:TokenExp"]));

            //claims rols
            var Mclaims = new List<Claim>(){
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName),
                       // new Claim(ClaimTypes.Email,user.Email),

                    };
            if (userRoles.Count > 0)
            {
                Mclaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
                //foreach (var role in userRoles)
                //{

                //    claims.Add(new Claim(ClaimTypes.Role, role));
                //}
            }

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: Configuration["Token:Issuer"],
                audience: Configuration["Token:Audience"],
                expires: tokenInstance.Expiration,//Token   5 dk  
                notBefore: DateTime.UtcNow,//Token start time
                signingCredentials: signingCredentials,
                claims: Mclaims
            );



            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            tokenInstance.AccessToken = tokenHandler.WriteToken(securityToken);

            //Refresh Token  
            tokenInstance.RefreshToken = CreateRefreshToken();
            return tokenInstance;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }
    }
}
