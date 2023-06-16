using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ComputerExam.Common
{
    public class AuthOptions
    {
        private readonly IConfiguration Configuration;

        public AuthOptions(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public JwtSecurityToken GetAuthData(IEnumerable<Claim> claims)
        {
            return new JwtSecurityToken(
                   issuer: Configuration["ISSUER"],
                   audience: Configuration["AUDIENCE"],
                   claims: claims,
                   expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(20)),
                   signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        }
        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new(Encoding.UTF8.GetBytes(Configuration["KEY"]));
        }
    }
}
