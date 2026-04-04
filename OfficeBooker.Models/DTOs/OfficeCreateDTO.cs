using System.ComponentModel.DataAnnotations;

namespace OfficeBooker.Models.DTOs
{
    public class OfficeCreateDTO
    {
        [Required]
        public int Capacity { get; set; }
        [Required]
         public int FloorNumber { get; set; }
        [Required]
        public int OfficeNumber { get; set; }
        public string Equipment { get; set; } = string.Empty;

    }
}
