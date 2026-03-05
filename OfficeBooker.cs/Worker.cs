using System.ComponentModel.DataAnnotations;

namespace OfficeBooker.Models.cs
{
    public class Worker
    {
        [Key]
        public int Id { get; set; } = default!;
        [Required]
        public string Name { get; set; } = default!;
        [Required]
        public string Surname { get; set; } = default!;
        List<Reservation> WorkerReservations { get; set; } = new List<Reservation>();
    }
}