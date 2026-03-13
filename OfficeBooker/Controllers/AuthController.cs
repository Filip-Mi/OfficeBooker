using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OfficeBooker.Models.cs;
using OfficeBooker.Models.cs.DTOs;
using System.Security.Claims;

namespace OfficeBooker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Worker> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthController(UserManager<Worker> userManager , RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateWorkerDTO model){
            var UserExist = await _userManager.FindByEmailAsync(model.Email);
            if (UserExist == null) { 
                Worker worker = new Worker{Name = model.Name, Surname = model.Surname  ,Email = model.Email};
               
                var result = await _userManager.CreateAsync(worker, password: model.Password);
                if (result.Succeeded) {
                    if (!await _roleManager.RoleExistsAsync(roleName: model.Role)){
                        await _roleManager.CreateAsync(new IdentityRole(model.Role));
                    }
                    await _userManager.AddToRoleAsync(worker, model.Role);
                    return Ok("Użytkownik zarejestrowany z rolą: " + model.Role);
                }

            };
            return BadRequest("");
        }
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByNameAsync(userId!);
            if (user == null) {
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
    }
}
