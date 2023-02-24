﻿using System;

namespace Crypto_Api.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(Exception ex) : base(ex.Message)
        {
        }

        public ValidationException() : base()
        {
        }
    }
}