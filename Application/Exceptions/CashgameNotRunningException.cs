using System;

namespace Application.Exceptions
{
    public class CashgameNotRunningException : Exception
    {
        public CashgameNotRunningException()
            : base("Cashgame is not running")
        {
        }
    }
}