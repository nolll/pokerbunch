using System;

namespace Core.Exceptions
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException(){ }
        public AccessDeniedException(string message) : base(message){ }
    }
}