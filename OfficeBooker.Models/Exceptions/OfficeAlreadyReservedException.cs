using OfficeBooker.Models.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeBooker.Models.Exceptions
{
    /// <summary>
    /// Exception thrown when a specific reservation already exist.
    /// Results in an HTTP 409 Not Found response.
    /// </summary>
    public class OfficeAlreadyReservedException : BaseDomainException
    {
        public OfficeAlreadyReservedException(int officeId)
        : base($"Office with ID {officeId} is already booked for this period.", 409)
        {
        }
    }
}
