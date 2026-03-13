
using System.ComponentModel.DataAnnotations;


namespace OfficeBooker.Models.cs.DTOs
{
    public class WorkerDTO
    {
        public string Id { get; set; } = default!;
        [Required]
        public string Name { get; set; } = default!;
        [Required]
        public string Surname { get; set; } = default!;
        [Required]
        public string Email { get; set; } = default!;
        public List<int> ReservationId { get; set; } = new List<int>();
    }
}
