using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OfficeBooker.Models.cs
{
    public class Reservation
    {
        [Key]
        int ReservationId {  get; set; }
        [Required]
        public int OfficeNumber { get; set; }
        [Required]
        public DateTime ReservationStartTime { get; set; }
        [Required]
        public DateTime ReservationEndTime { get; set; }
        [Required]
        public DateTime ReservationCreateTime { get; set; } = DateTime.Now;
        [Required]
        public int WorkerId{ get; set;}
        [Required]
        public string WorkerName{ get; set;}
    }
}
