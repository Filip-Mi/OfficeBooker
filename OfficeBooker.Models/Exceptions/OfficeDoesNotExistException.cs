using OfficeBooker.Models.Exceptions.Base;


namespace OfficeBooker.Models.Exceptions
{
    /// <summary>
    /// Exception thrown when the specified office does not exist.
    /// Results in an HTTP 404 Not Found response.
    /// </summary>
    public class OfficeDoesNotExistException : BaseDomainException
    {
        public OfficeDoesNotExistException(int officeId) : base($"Office with ID {officeId} does not exist.", 404)
        {
        }
    }
}
