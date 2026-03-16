using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeBooker.Models.cs.DTOs
{
    public class AuthResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}
