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

        /// <summary>
        /// Creates a new reservation for the currently authenticated user.
        /// </summary>
        /// <param name="dto">Reservation details including office ID and requested time slot.</param>
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

        /// <summary>
        /// Retrieves all reservations associated with the logged-in user.
        /// </summary>
        [HttpGet("my")]
        public async Task<IActionResult> GetMyReservations()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var reservations = await _reservationService.GetMyReservationsAsync(userId);
            return Ok(reservations);
        }

        /// <summary>
        /// Cancels an existing reservation. Only the owner of the reservation can perform this action.
        /// </summary>
        /// <param name="id">The unique ID of the reservation to be removed.</param>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var userId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();
            await _reservationService.DeleteReservationAsync(id, userId);
            return NoContent();
        }
        /// <summary>
        /// Updates an existing reservation. Only the owner of the reservation can perform this action.
        /// </summary>
        /// <param name="id">The unique ID of the reservation to be updated.</param>
        /// /// <param name="reservation">The reservation details to be updated.</param>
       [HttpPut]
       [Route("update")]
        public async Task<IActionResult> UpdateReservation(int id, [FromBody] ReservationUpdateDTO reservation)
        {
            var userId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();
            await _reservationService.UpdateReservationAsync(id, reservation, userId);
            return NoContent();
        }
    }
}