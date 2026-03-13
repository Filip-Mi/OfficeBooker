using System.ComponentModel.DataAnnotations;

namespace OfficeBooker.Models.cs.DTOs
{
    public class CreateWorkerDTO
    {
        [Required]
        public string Name { get; set; } = default!;
        [Required]
        public string Surname { get; set; } = default!;
        [Required]
        public string Email { get; set; } = default!;
        [Required]
        [MinLength(6)]
        public string Password { get; set; } = default!;
        [Required]
        public string Role { get; set; } = "User";
    }
}
