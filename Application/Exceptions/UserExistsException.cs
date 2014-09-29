using System;

namespace Application.Exceptions
{
    public class UserExistsException : Exception
    {
        public UserExistsException()
            : base(("The User Name is in use"))
        {
        }
    }
}