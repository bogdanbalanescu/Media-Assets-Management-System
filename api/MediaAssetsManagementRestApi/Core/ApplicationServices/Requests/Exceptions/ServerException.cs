using System;

namespace ApplicationServices.Requests.Exceptions
{
    public class ServerException : Exception
    {
        public ServerException(string message) : base(message)
        {
        }
    }
}
