using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OfficeBooker.Models.cs;
using OfficeBooker.Models.cs.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OfficeBooker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Worker> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthController(UserManager<Worker> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateWorkerDTO model)
        {
            var UserExist = await _userManager.FindByEmailAsync(model.Email);
            if (UserExist == null)
            {
                Worker worker = new Worker { Name = model.Name, Surname = model.Surname, Email = model.Email };

                var result = await _userManager.CreateAsync(worker, password: model.Password);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(roleName: model.Role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(model.Role));
                    }
                    await _userManager.AddToRoleAsync(worker, model.Role);
                    return Ok("Użytkownik zarejestrowany z rolą: " + model.Role);
                }

            }
            ;
            return BadRequest("");
        }
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByNameAsync(userId!);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(new
            {
                user.Email,
                user.Name,
                user.Surname,
                Roles = await _userManager.GetRolesAsync(user)
            });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginWorkerDTO loginworker)
        {
            var user = await _userManager.FindByEmailAsync(loginworker.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginworker.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var authSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    expires: DateTime.Now.AddDays(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return Unauthorized();
        }

    }
}
