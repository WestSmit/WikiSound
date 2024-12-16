using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WikiSound.Server.Configurations
{
    public class JwtTokenConfiguration
    {
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateLifetime { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public int ExpirationMinutes { get; set; } = 30;
        public string IssuerSigningKeySecret { get; set; }
        public SymmetricSecurityKey IssuerSigningKey
        {
            get
            {
                return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IssuerSigningKeySecret));
            }
        }

        public TokenValidationParameters CreateValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = ValidateIssuer,
                ValidateAudience = ValidateAudience,
                ValidateLifetime = ValidateLifetime,
                ValidateIssuerSigningKey = ValidateIssuerSigningKey,
                ValidIssuer = ValidIssuer,
                ValidAudience = ValidAudience,
                IssuerSigningKey = IssuerSigningKey
            };
        }
    }
}
