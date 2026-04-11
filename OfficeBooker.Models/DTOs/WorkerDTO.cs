using System.ComponentModel.DataAnnotations;

namespace OfficeBooker.Models.DTOs
{
    /// <summary>
    /// Data transfer object representing basic information about a worker.
    /// </summary>
    public class WorkerDTO
    {
        /// <summary>
        /// The unique internal identifier for the worker.
        /// </summary>
        public string Id { get; set; } = default!;

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
        /// The worker's corporate email address.
        /// </summary>
        [Required]
        public string Email { get; set; } = default!;

        /// <summary>
        /// A list of unique identifiers for all reservations made by this worker.
        /// </summary>
        public List<int> ReservationId { get; set; } = new List<int>();
    }
}