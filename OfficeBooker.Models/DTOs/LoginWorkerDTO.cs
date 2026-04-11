namespace OfficeBooker.Models.DTOs
{
    /// <summary>
    /// Data transfer object used for worker authentication.
    /// </summary>
    public class LoginWorkerDTO
    {
        /// <summary>
        /// The worker's registered email address.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// The secret password associated with the worker's account.
        /// </summary>
        public string? Password { get; set; }
    }
}