using System.ComponentModel.DataAnnotations;

namespace OfficeBooker.Models.DTOs
{
    /// <summary>
    /// Data transfer object for registering a new worker account.
    /// </summary>
    public class CreateWorkerDTO
    {
        /// <summary>
        /// The worker's first name.
        /// </summary>
        [Required]
        public string Name { get; set; } = default!;

        /// <summary>
        /// The worker's last name.
        /// </summary>
        [Required]
        public string Surname { get; set; } = default!;

        /// <summary>
        /// Corporate email address that will be used as a login identifier.
        /// </summary>
        [Required]
        public string Email { get; set; } = default!;

        /// <summary>
        /// Secure password for the new account. Must be at least 6 characters long.
        /// </summary>
        [Required]
        [MinLength(6)]
        public string Password { get; set; } = default!;

        /// <summary>
        /// The assigned role for the user (e.g., User, Admin). Defaults to "User".
        /// </summary>
        [Required]
        public string Role { get; set; } = "User";
    }
}