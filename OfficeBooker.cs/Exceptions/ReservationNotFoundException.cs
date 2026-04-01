using OfficeBooker.Models.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeBooker.Models.Exceptions
/// <summary>
/// Exception thrown when a specific reservation cannot be located in the system.
/// Results in an HTTP 404 Not Found response.
/// </summary>
{
    public class ReservationNotFoundException : BaseDomainException
    {
        public ReservationNotFoundException(int reservationId)
            : base($"Reservation with ID {reservationId} was not found.", 404)
        {
        }
    }
}
