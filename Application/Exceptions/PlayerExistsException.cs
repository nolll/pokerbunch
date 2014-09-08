using System;

namespace Application.Exceptions
{
    public class PlayerExistsException : Exception
    {
        public PlayerExistsException()
            : base(("The Display Name is in use by someone else"))
        {
        }
    }
}