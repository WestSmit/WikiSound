using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WikiSound.Server.Configurations;

namespace WikiSound.Server.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtTokenConfiguration _jwtTokenConfig;

        public TokenService(JwtTokenConfiguration jwtTokenConfig)
        {
            _jwtTokenConfig = jwtTokenConfig;
        }

        public string CreateToken(IdentityUser user)
        {
            var expiration = DateTime.UtcNow.AddMinutes(_jwtTokenConfig.ExpirationMinutes);

            var token = CreateJwtToken(
                CreateClaims(user),
                new SigningCredentials(_jwtTokenConfig.IssuerSigningKey, SecurityAlgorithms.HmacSha256),
                expiration
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials, DateTime expiration)
        {
            return new(
                _jwtTokenConfig.ValidIssuer,
                _jwtTokenConfig.ValidAudience,
                claims,
                expires: expiration,
                signingCredentials: credentials
            );
        }

        private List<Claim> CreateClaims(IdentityUser user)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, "TokenForTheApiWithAuth"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
                };
                return claims;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
