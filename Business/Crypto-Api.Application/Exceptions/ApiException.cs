using System;
using System.Globalization;

namespace Crypto_Api.Application.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException() : base()
        {
        }

        public ApiException(string message) : base(message)
        {
        }

        public ApiException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture,
            message, args))
        {
        }
    }
}