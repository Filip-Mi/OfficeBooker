using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeBooker.Models.DTOs;
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
        /// <summary>
        /// Registers a new worker account in the system.
        /// </summary>
        /// <param name="model">New worker details including email, password, and personal info.</param>
        /// <returns>A success message upon successful registration.</returns>
        /// <response code="200">Worker registered successfully.</response>
        /// <response code="400">If validation fails or user already exists.</response>
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
        /// <summary>
        /// Retrieves the profile information of the currently authenticated worker.
        /// </summary>
        /// <returns>Worker profile data based on the JWT token claims.</returns>
        /// <response code="200">Returns the current user's profile.</response>
        /// <response code="401">If the request is not authenticated.</response>
        /// <response code="404">If the user is not found in the database.</response>
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();
            var user = await _authService.GetCurrentUserAsync(userId);

            return user != null ? Ok(user) : NotFound();
        }
        /// <summary>
        /// Authenticates a worker and returns a JWT token.
        /// </summary>
        /// <param name="model">The login credentials.</param>
        /// <returns>An authentication response containing the token and expiration date.</returns>
        /// <response code="200">Returns the JWT token.</response>
        /// <response code="401">If the credentials are invalid.</response>
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
