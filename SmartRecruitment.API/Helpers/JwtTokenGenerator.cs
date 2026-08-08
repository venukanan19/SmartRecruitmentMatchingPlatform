using Microsoft.IdentityModel.Tokens;
using SmartRecruitment.API.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartRecruitment.API.Helpers
{
    public class JwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public (string Token, DateTime ExpiresAt) GenerateToken(User user)
        {
            string key = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException(
                    "JWT signing key is not configured.");

            string issuer = _configuration["Jwt:Issuer"]
                ?? throw new InvalidOperationException(
                    "JWT issuer is not configured.");

            string audience = _configuration["Jwt:Audience"]
                ?? throw new InvalidOperationException(
                    "JWT audience is not configured.");

            int expiryMinutes =
                _configuration.GetValue<int?>("Jwt:ExpiryMinutes") ?? 60;

            DateTime expiresAt =
                DateTime.UtcNow.AddMinutes(expiryMinutes);

            Claim[] claims =
            [
                new Claim(
                    JwtRegisteredClaimNames.Sub,
                    user.UserId.ToString()),

                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.UserId.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    user.FullName),

                new Claim(
                    ClaimTypes.Email,
                    user.Email),

                new Claim(
                    ClaimTypes.Role,
                    user.Role),

                new Claim(
                    JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString())
            ];

            SymmetricSecurityKey securityKey =
                new(Encoding.UTF8.GetBytes(key));

            SigningCredentials credentials =
                new(
                    securityKey,
                    SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expiresAt,
                signingCredentials: credentials);

            string tokenValue =
                new JwtSecurityTokenHandler().WriteToken(token);

            return (tokenValue, expiresAt);
        }
    }
}
