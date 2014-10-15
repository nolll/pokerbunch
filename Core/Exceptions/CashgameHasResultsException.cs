using System;

namespace Core.Exceptions
{
    public class CashgameHasResultsException : Exception
    {
        public CashgameHasResultsException()
            : base("Cashgames with results can't be deleted.")
        {
        }
    }
}