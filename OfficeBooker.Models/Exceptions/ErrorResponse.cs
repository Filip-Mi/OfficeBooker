#nullable enable

using OfficeBooker;
using System;
using System.Collections.Generic;

namespace OfficeBooker.Models.Exceptions
{
    public class ErrorResponse
    {
        /// <summary>
        /// Error message for the user
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Error code (e.g., "VALIDATION_ERROR", "UNAUTHORIZED", "NOT_FOUND")
        /// </summary>
        public string ErrorCode { get; set; } = string.Empty;

        /// <summary>
        /// HTTP response status code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// The endpoint path where the error occurred
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// The timestamp when the error occurred
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Detailed error information
        /// </summary>
        public string? Details { get; set; }

        /// <summary>
        /// Validation errors (for form validation failures)
        /// </summary>
        public Dictionary<string, List<string>>? ValidationErrors { get; set; }

        /// <summary>
        /// Unique transaction ID for error tracking
        /// </summary>
        public string? TraceId { get; set; }
    }
}
