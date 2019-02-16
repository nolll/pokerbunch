using System;

namespace PokerBunch.Client.Exceptions
{
    public class PokerBunchException : Exception
    {
        protected PokerBunchException()
        {
        }

        protected PokerBunchException(string message) : base(message)
        {
        }
    }
}