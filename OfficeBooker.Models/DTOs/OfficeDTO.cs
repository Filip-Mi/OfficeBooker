using System.ComponentModel.DataAnnotations;

namespace OfficeBooker.Models.DTOs
{
    public class OfficeDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int OfficeNumber { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        public int FloorNumber { get; set; }
        public string Equipment { get; set; } = default!;
    }
}
