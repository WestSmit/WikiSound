using Blazored.LocalStorage;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using WikiSound.Shared.Auth;

namespace WikiSound.Client.Services
{
    internal class AuthService : BaseHttpService, IAuthService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;
        private const string TokenKey = "token";

        public event Action OnAuthenticationStateChanged;

        public IdentityUser? CurrentUser { get; private set; }

        public AuthService(
            IWebAssemblyHostEnvironment hostEnvironmen,
            IConfiguration config,
            ILocalStorageService localStorageService,
            HttpClient http) : base(hostEnvironmen, config)
        {
            _http = http;
            _localStorage = localStorageService;
        }

        public async Task<AuthResponse> RegisterAsync(RegistrationRequest request)
        {
            var url = new Uri(_apiUrl, "api/auth/register");

            try
            {
                var response = await _http.PostAsJsonAsync(url, request);

                var result = await response.Content.ReadFromJsonAsync<AuthResponse>();

                if (result.Succeeded)
                {
                    await _localStorage.SetItemAsync(TokenKey, result.Token);
                    await GetUser(result.Email, result.Token);
                    OnAuthenticationStateChanged();
                }

                return result;
            }
            catch (Exception ex)
            {
                await _localStorage.RemoveItemAsync(TokenKey);
                return new AuthResponse
                {
                    Succeeded = false,
                    Errors = new Dictionary<string, string[]>() { { "Error", new[] { "Sorry, we were unable to register you at this time. Please try again shortly." } } }
                };
            }
        }

        public async Task<AuthResponse> LoginAsync(AuthRequest request)
        {
            var url = new Uri(_apiUrl, "api/auth/login");

            try
            {
                var response = await _http.PostAsJsonAsync(url, request);

                var result = await response.Content.ReadFromJsonAsync<AuthResponse>();

                if (result.Succeeded)
                {
                    await _localStorage.SetItemAsync(TokenKey, result.Token);
                    await GetUser(result.Email, result.Token);
                    OnAuthenticationStateChanged();
                }

                return result;
            }
            catch (Exception ex)
            {
                await _localStorage.RemoveItemAsync(TokenKey);
                return new AuthResponse
                {
                    Succeeded = false,
                    Errors = new Dictionary<string, string[]>() { { "Error", new[] { "Sorry, we were unable to log you in at this time. Please try again shortly." } } }
                };
            }
        }

        public async Task<ClaimsIdentity> GetClaimsIdentity()
        {
            var tokenValue = await _localStorage.GetItemAsync<string>(TokenKey);

            var claims = new List<Claim>();

            if (tokenValue == null)
            {
                return new ClaimsIdentity(claims, null);
            }

            claims = new JwtSecurityTokenHandler()
                .ReadJwtToken(tokenValue)
                .Claims
                .ToList();

            var email = claims
                .Single(x => x.Type == ClaimTypes.Email)
                .Value;

            if (CurrentUser == null || CurrentUser.Email != email)
            {
                await GetUser(email, tokenValue);
            }

            if (CurrentUser != null)
            {
                return new ClaimsIdentity(claims, "Server authentication");
            }

            return new ClaimsIdentity(claims, null);
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync(TokenKey);

            CurrentUser = null;

            OnAuthenticationStateChanged();
        }

        private async Task GetUser(string email, string token)
        {
            var url = new Uri(_apiUrl, $"api/users?email={email}");

            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var user = await _http.GetFromJsonAsync<IdentityUser>(url);

                if (user == null)
                {
                    await LogoutAsync();
                }

                CurrentUser = user;
            }
            catch (HttpRequestException e)
            {
                await LogoutAsync();
            }
        }
    }
}