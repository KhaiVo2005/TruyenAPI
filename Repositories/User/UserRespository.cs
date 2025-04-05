using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TruyenAPI.Repositories.User
{
    public class UserRespository: IUserRespository
    {
        private readonly IConfiguration configuration;

        public UserRespository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CreateJwtToken(IdentityUser user, List<string> role)
        {
            var claim = new List<Claim>();
            claim.Add(new Claim(ClaimTypes.Email, user.Email));
            foreach (var roleClaim in role)
            {
                claim.Add(new Claim(ClaimTypes.Role, roleClaim));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]));
            var cre = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claim,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: cre
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
