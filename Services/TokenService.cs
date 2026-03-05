using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Interfaces.Services;
using TaskManager.Models.Entities;

namespace TaskManager.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(User user)
        {
            var secretKey = _configuration["JwtSettings:SecretKey"];
            var chaveConvertida = Encoding.ASCII.GetBytes(secretKey);

            var idClaim = new Claim(ClaimTypes.NameIdentifier, user.Id.ToString());
            var emailClaim = new Claim(ClaimTypes.Email, user.Email);
            var nameClaim = new Claim(ClaimTypes.Name, user.Name);

            var tokenSecurity = new SecurityTokenDescriptor();

            tokenSecurity.Subject = new ClaimsIdentity(new[] { idClaim, emailClaim, nameClaim });
            tokenSecurity.Expires = DateTime.UtcNow.AddHours(2);
            tokenSecurity.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chaveConvertida), SecurityAlgorithms.HmacSha256Signature);

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenSecurity);

            return tokenHandler.WriteToken(token);
        }
    }
}
