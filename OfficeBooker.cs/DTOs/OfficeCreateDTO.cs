using System.ComponentModel.DataAnnotations;

namespace OfficeBooker.Models.DTOs
{
    public class OfficeCreateDTO
    {
        [Required]
        public int OfficeNumber { get; set; } 
        [Required]
        public int Capacity { get; set; }
        [Required]
         public int FloorNumber { get; set; }
        public List<string> Equipment { get; set; } = new List<string>();

    }
}
