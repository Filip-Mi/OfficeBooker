using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeBooker.Models.cs.DTOs;
using OfficeBooker.Services.IServices;
using System.Security.Claims;

namespace OfficeBooker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateWorkerDTO model)
        {
            var result = await _authService.RegisterAsync(model);
            if (!result.Success)
            {
                return BadRequest();
            }
            return Ok(result.Message);
        }
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();
            var user = await _authService.GetCurrentUserAsync(userId);

            return user != null ? Ok(user) : NotFound();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginWorkerDTO loginworker)
        {
            var response = await _authService.LoginAsync(loginworker);

            if (response == null)
            {
                return Unauthorized();
            }   

            return Ok(response);
        }
    }
}
