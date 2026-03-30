using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeBooker.DataAccess.Repository.I_Repository;
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
        public readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationCreateDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            try
            {
                var result = await _reservationService.CreateReservationAsync(dto, userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("my")]
        public async Task<IActionResult> GetMyReservations()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var reservations = await _reservationService.GetMyReservationsAsync(userId);
            return Ok(reservations);
        }
    }
}
