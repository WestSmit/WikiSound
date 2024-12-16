using System.Security.Claims;
using WikiSound.Shared.Auth;

namespace WikiSound.Client.Services
{
    internal interface IAuthService
    {
        event Action OnAuthenticationStateChanged;
        Task<AuthResponse> RegisterAsync(RegistrationRequest request);
        Task<AuthResponse> LoginAsync(AuthRequest request);
        Task<ClaimsIdentity> GetClaimsIdentity();
        Task LogoutAsync();

    }
}