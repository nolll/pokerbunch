using System;

namespace Core.Exceptions
{
    public class BunchExistsException : Exception
    {
        public BunchExistsException()
            : base("The Bunch name is not available")
        {
        }
    }
}