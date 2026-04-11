using System.ComponentModel.DataAnnotations;

namespace OfficeBooker.Models.DTOs
{
    /// <summary>
    /// Data transfer object for creating a new office space.
    /// </summary>
    public class OfficeCreateDTO
    {
        /// <summary>
        /// The maximum number of desks or people the office can accommodate.
        /// </summary>
        [Required]
        public int Capacity { get; set; }

        /// <summary>
        /// The floor number where the office is located.
        /// </summary>
        [Required]
        public int FloorNumber { get; set; }

        /// <summary>
        /// The specific office number or designation.
        /// </summary>
        [Required]
        public int OfficeNumber { get; set; }

        /// <summary>
        /// A description of available office equipment (e.g., Whiteboard, Projector, AC).
        /// </summary>
        public string Equipment { get; set; } = string.Empty;
    }
}