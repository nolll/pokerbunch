using System;

namespace Core.Exceptions
{
    public class CashgameNotRunningException : Exception
    {
        public CashgameNotRunningException()
            : base("Cashgame is not running")
        {
        }
    }
}