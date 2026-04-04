using OfficeBooker.Models.Exceptions.Base;

namespace OfficeBooker.Models.Exceptions
{
    /// <summary>
    /// Exception thrown when the provided reservation time range is logically invalid 
    /// Results in an HTTP 400 Bad Request response.
    /// </summary>
    public class InvalidReservationPeriodException : BaseDomainException
    {
        public InvalidReservationPeriodException(DateTime start, DateTime end)
            : base($"The reservation period is invalid. Start: {start:G}, End: {end:G}. End time must be after start time.", 400)
        {
        }
        public InvalidReservationPeriodException(string message) : base(message, 400) { }
    }
}
