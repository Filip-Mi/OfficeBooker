using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OfficeBooker.Models.cs.DTOs
{
    public  class LoginworkerDTO
    {
        [Required]
        public string Email { get; set; } = default!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
        
    }
}
