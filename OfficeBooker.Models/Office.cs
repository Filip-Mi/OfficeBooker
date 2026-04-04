using System.ComponentModel.DataAnnotations;

namespace OfficeBooker.Models;

    public class Office
    {
        [Key]
        public int Id { get; set;}
        [Required]
        public int OfficeNumber { get; set;}
        [Required]
        public int  Capacity{ get; set;}
        [Required]
        public int FloorNumber {  get; set;}
        public string Equipment { get; set; } = default!;
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}