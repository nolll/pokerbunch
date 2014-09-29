using System;

namespace Application.Exceptions
{
    public class EmailExistsException : Exception
    {
        public EmailExistsException()
            : base(("The Email Address is in use"))
        {
        }
    }
}