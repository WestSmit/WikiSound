using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WikiSound.Server.Services;

namespace WikiSound.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) 
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
