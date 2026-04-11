using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OfficeBooker.Models.DTOs
{
    /// <summary>
    /// Data transfer object representing the full details of an existing reservation.
    /// </summary>
    public class ReservationRespondDTO
    {
        /// <summary>
        /// The unique internal identifier for the reservation.
        /// </summary>
        [Required]
        public int Id { get; init; }

        /// <summary>
        /// The unique identifier of the worker who owns this reservation.
        /// </summary>
        [Required]
        public string WorkerId { get; init; } = string.Empty;

        /// <summary>
        /// The date and time when the reservation begins.
        /// </summary>
        [Required]
        public DateTime ReservationStartTime { get; init; }

        /// <summary>
        /// The date and time when the reservation ends.
        /// </summary>
        [Required]
        public DateTime ReservationEndTime { get; init; }

        /// <summary>
        /// The timestamp indicating when the reservation was originally created in the system.
        /// </summary>
        [Required]
        public DateTime ReservationCreateTime { get; init; }

        /// <summary>
        /// The unique internal identifier of the reserved office.
        /// </summary>
        [Required]
        public int OfficeId { get; init; }

        /// <summary>
        /// The friendly office number or designation for easy identification.
        /// </summary>
        [Required]
        public int OfficeNumber { get; set; }

        /// <summary>
        /// The floor level where the reserved office is located.
        /// </summary>
        [Required]
        public int FloorNumber { get; set; }
    }
}