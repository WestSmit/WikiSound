using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace WikiSound.Client.Services
{
    internal class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthService _auth;
        public CustomAuthenticationStateProvider(IAuthService auth)
        {
            _auth = auth;

            _auth.OnAuthenticationStateChanged += () => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return new AuthenticationState(new ClaimsPrincipal(await _auth.GetClaimsIdentity()));
        }
    }
}
