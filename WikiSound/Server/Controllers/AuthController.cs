using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WikiSound.Server.Services;
using WikiSound.Shared.Auth;

namespace WikiSound.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userManager.CreateAsync(
                new IdentityUser { UserName = request.Username, Email = request.Email },
                request.Password
            );

            if (result.Succeeded)
            {
                var managedUser = await _userManager.FindByEmailAsync(request.Email);

                var accessToken = _tokenService.CreateToken(managedUser);

                return CreatedAtAction(
                    nameof(Register),
                    new { email = request.Email },
                    new AuthResponse {
                        Email = request.Email,
                        Username = request.Username,
                        Token = accessToken,
                        Succeeded = true,
                        });
            }

            return BadRequest(new AuthResponse
            {
                Succeeded = false,
                Errors = new Dictionary<string, string[]> { { string.Empty, result.Errors.Select(x => x.Description).ToArray()} }
            }); ;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AuthRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var managedUser = await _userManager.FindByEmailAsync(request.Email);

            if (managedUser == null)
            {
                return BadRequest(new AuthResponse {
                    Succeeded = false,
                    Errors = new Dictionary<string, string[]>() { { "Credentials", new[] { "Bad credentials" } } } });
            }

            var passwordValidation = await _signInManager.CheckPasswordSignInAsync(managedUser, request.Password, false);

            if (!passwordValidation.Succeeded)
            {
                return BadRequest(new AuthResponse {
                    Succeeded = false,
                    Errors = new Dictionary<string, string[]>() { { "Credentials", new[] { "Bad credentials" } } } });
            }

            var accessToken = _tokenService.CreateToken(managedUser);

            return Ok(new AuthResponse
            {
                Succeeded = true,
                Username = managedUser.UserName,
                Email = managedUser.Email,
                Token = accessToken,
            });
        }
    }
}
