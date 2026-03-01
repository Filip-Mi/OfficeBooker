using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OfficeBooker.Models.cs
{
    public class Reservation
    {
        [Key]
        public int Id {  get; set; }
        [Required]
        public int OfficeId { get; set; }
        [ForeignKey("OfficeId")]
        public Office? Office { get; set; }
        [Required]
        public DateTime ReservationStartTime { get; set; }
        [Required]
        public DateTime ReservationEndTime { get; set; }
        [Required]
        public DateTime ReservationCreateTime { get; set; } = DateTime.Now;
        [Required]
       
        public int WorkerId{ get; set;}
        [ForeignKey("WorkerId")]
        public Worker? Worker { get; set;}
        [Required]
        public string WorkerName{ get; set;} = default!;
    }
}
