using System.ComponentModel.DataAnnotations;

namespace OfficeBooker.Models.DTOs
{
    /// <summary>
    /// Data transfer object representing a detailed office record.
    /// </summary>
    public class OfficeDTO
    {
        /// <summary>
        /// The unique internal identifier for the office.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// The specific office number or designation (e.g., 101, A-12).
        /// </summary>
        [Required]
        public int OfficeNumber { get; set; }

        /// <summary>
        /// The maximum number of work stations available in this office.
        /// </summary>
        [Required]
        public int Capacity { get; set; }

        /// <summary>
        /// The floor level within the building.
        /// </summary>
        [Required]
        public int FloorNumber { get; set; }

        /// <summary>
        /// A list of available amenities such as monitors, docks, or specialized chairs.
        /// </summary>
        public string Equipment { get; set; } = default!;
    }
}