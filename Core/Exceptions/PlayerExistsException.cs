using System;

namespace Core.Exceptions
{
    public class PlayerExistsException : Exception
    {
        public PlayerExistsException()
            : base(("The Display Name is in use by someone else"))
        {
        }
    }
}