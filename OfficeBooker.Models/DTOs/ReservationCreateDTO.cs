using System.ComponentModel.DataAnnotations;

namespace OfficeBooker.Models.DTOs
{
    /// <summary>
    /// Data transfer object for creating a new office desk reservation.
    /// </summary>
    public class ReservationCreateDTO
    {
        /// <summary>
        /// The unique identifier of the office to be reserved.
        /// </summary>
        [Required]
        public int OfficeId { get; set; } = default!;

        /// <summary>
        /// The scheduled start date and time for the reservation.
        /// </summary>
        [Required]
        public DateTime ReservationStartTime { get; set; } = default!;

        /// <summary>
        /// The scheduled end date and time for the reservation.
        /// </summary>
        [Required]
        public DateTime ReservationEndTime { get; set; } = default!;
    }
}