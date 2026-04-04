using System.ComponentModel.DataAnnotations;

namespace OfficeBooker.Models.DTOs
{
    public class ReservationCreateDTO
    {
        [Required]
        public int OfficeId { get; set; } = default!;
        [Required]
        public DateTime ReservationStartTime { get; set; } = default!;
        [Required]
        public DateTime ReservationEndTime { get; set; }  = default!;
    }
}
