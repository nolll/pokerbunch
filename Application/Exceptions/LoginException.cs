using System;

namespace Application.Exceptions
{
    public class LoginException : Exception
    {
        public LoginException()
            : base(("There was something wrong with your username or password. Please try again."))
        {
        }
    }
}