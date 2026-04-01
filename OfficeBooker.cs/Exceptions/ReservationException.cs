namespace OfficeBooker.Models.Exceptions
{
    /// <summary>
    /// Exception thrown when a reservation operation fails.
    /// </summary>
    public class ReservationException : Exception
    {
        public ReservationException(string message) : base(message) { }
        public ReservationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
