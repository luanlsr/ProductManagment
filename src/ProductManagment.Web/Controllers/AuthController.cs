using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductManagment.Application.DTOs.Auth;
using ProductManagment.Domain.Entities.Identity;
using ProductManagment.Web.Helper;

namespace ProductManagment.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserDTO dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Name,
                NormalizedUserName = dto.Email.ToUpper(),
                Email = dto.Email,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("User Registered Successfully.");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUserDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Unauthorized("Invalid Credentials.");

            var check = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!check.Succeeded)
                return Unauthorized("Invalid Credentials.");

            // Gera Token JWT
            var token = await JwtTokenHelper.GenerateJwtToken(user, _userManager, _configuration);
            return Ok(new { token });
        }
    }
}
