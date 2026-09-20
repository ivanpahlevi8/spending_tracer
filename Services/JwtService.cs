using Microsoft.IdentityModel.Tokens;
using SpendTracerApi.Models;
using SpendTracerApi.Services.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SpendTracerApi.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(UserModel userModel)
        {
            try
            {
                // get Jwt Setting
                var getConfiguration = _configuration.GetSection("JwtSettings");

                if (getConfiguration == null)
                {
                    throw new Exception("Configuration does not exist.");
                }

                // 1. Corrected key mappings
                var getIssuer = getConfiguration["Issuer"];
                var getSecret = getConfiguration["Secret"];
                var getAudience = getConfiguration["Audience"];
                var getExpiry = getConfiguration["ExpiryMinutes"];

                // Check if each entry was provided
                if (string.IsNullOrEmpty(getIssuer) || string.IsNullOrEmpty(getSecret) ||
                    string.IsNullOrEmpty(getAudience) || string.IsNullOrEmpty(getExpiry))
                {
                    throw new Exception("Configuration file for JWT is missing required settings.");
                }

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(getSecret));
                var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                // Create claims
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userModel.UserName ?? string.Empty),
                    new Claim(JwtRegisteredClaimNames.Email, userModel.Email ?? string.Empty),
                    new Claim(JwtRegisteredClaimNames.Name, userModel.FirstName ?? string.Empty),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                // 2. Added signingCredentials and fixed syntax
                var token = new JwtSecurityToken(
                    issuer: getIssuer,
                    audience: getAudience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(double.Parse(getExpiry)),
                    signingCredentials: credentials
                );

                // Write the token string
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return tokenString;
            } catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
