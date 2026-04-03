using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeBooker.Models.DTOs;
using OfficeBooker.Services.IServices;
using System.Security.Claims;

namespace OfficeBooker.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationCreateDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _reservationService.CreateReservationAsync(dto, userId);
            return Ok(result);
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyReservations()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var reservations = await _reservationService.GetMyReservationsAsync(userId);
            return Ok(reservations);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var userId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();
            await _reservationService.DeleteReservationAsync(id, userId);
            return NoContent();
        }
    }
}
