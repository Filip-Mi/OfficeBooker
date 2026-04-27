using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OfficeBooker.Models.DTOs
{
    /// <summary>
    /// Data transfer object for updating an existing office desk reservation.
    /// </summary>
    public class ReservationUpdateDTO
    {
        /// <summary>
        /// The unique identifier of the office to be reserved.
        /// </summary>
        public int? OfficeId { get; set; }

        /// <summary>
        /// The scheduled start date and time for the reservation.
        /// </summary>
        public DateTime? ReservationStartTime { get; set; }     

        /// <summary>
        /// The scheduled end date and time for the reservation.
        /// </summary>
        public DateTime? ReservationEndTime { get; set; }
    }
}
