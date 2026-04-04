using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeBooker.Models.Exceptions.Base
{
    public abstract class BaseDomainException : Exception
    {
        public int StatusCode { get; }

        protected BaseDomainException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
