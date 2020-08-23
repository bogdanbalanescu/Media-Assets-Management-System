using System;

namespace Domain.SharedKernel.Exceptions
{
    public class RequiredArgumentException : ArgumentException
    {
        public RequiredArgumentException(string message) : base(message)
        {
        }
    }
}
