using Microsoft.AspNetCore.Identity;

namespace WikiSound.Server.Services
{
    public interface ITokenService
    {
        string CreateToken(IdentityUser user);
    }
}