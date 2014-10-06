using System;

namespace Core.Exceptions
{
    public class CashgameNotFoundException : Exception
    {
        public CashgameNotFoundException()
            : base(("Cashgame not found"))
        {
        }
    }
}