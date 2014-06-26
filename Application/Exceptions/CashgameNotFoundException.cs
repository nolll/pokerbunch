using System;

namespace Application.Exceptions
{
    public class CashgameNotFoundException : Exception
    {
        public CashgameNotFoundException()
            : base(("Cashgame not found"))
        {
        }
    }
}