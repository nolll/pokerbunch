using System.Collections.Generic;

namespace PokerBunch.Client.Exceptions
{
    public class ValidationException : PokerBunchException
    {
        public IEnumerable<string> Messages { get; }

        public ValidationException(string message)
        {
            Messages = new List<string> { message };
        }
    }
}