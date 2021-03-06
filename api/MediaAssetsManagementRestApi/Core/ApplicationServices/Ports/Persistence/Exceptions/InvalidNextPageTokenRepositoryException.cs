﻿using Domain.SharedKernel.Exceptions;

namespace ApplicationServices.Ports.Persistence.Exceptions
{
    public class InvalidNextPageTokenRepositoryException : RepositoryException
    {
        public InvalidNextPageTokenRepositoryException() : base()
        {
        }

        public InvalidNextPageTokenRepositoryException(string message) : base(message)
        {
        }
    }
}
