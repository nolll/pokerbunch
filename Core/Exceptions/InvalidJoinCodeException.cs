using System;

namespace Core.Exceptions
{
    public class InvalidJoinCodeException : Exception
    {
        public InvalidJoinCodeException()
            : base("That code didn't work. Please check for errors and try again")
        {
        }
    }
}