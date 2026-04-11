using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeBooker.Models.DTOs
{
    /// <summary>
    /// Represents the authentication response containing the security token and its validity period.
    /// </summary>
    public class AuthResponseDTO
    {
        /// <summary>
        /// The JSON Web Token (JWT) used for authenticating subsequent API requests.
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// The date and time when the authentication token expires.
        /// </summary>
        public DateTime Expiration { get; set; }
    }
}