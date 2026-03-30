using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OfficeBooker.Models.DTOs
{
    public class ReservationRespondDTO
    {
        [Required]
        public int Id { get; init; }
        [Required]
        public string WorkerId { get; init; } = string.Empty;
        [Required]
        public DateTime ReservationStartTime { get; init; }
        [Required]
        public DateTime ReservationEndTime { get; init; }
        [Required]
        public DateTime ReservationCreateTime { get; init; }
        [Required]
        public int OfficeId { get; init; }
        [Required]
        public int  OfficeNumber { get; init; } 
        [Required]
        public int FloorNumber { get; init; }
    }
}
