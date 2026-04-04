using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OfficeBooker.Models
{
    public class Worker : IdentityUser
    {
        [Required]
        public string Name { get; set; } = default!;
        [Required]
        public string Surname { get; set; } = default!;
        List<Reservation> WorkerReservations { get; set; } = new List<Reservation>();
    }
}