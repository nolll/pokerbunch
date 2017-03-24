using System.Collections.Generic;

namespace Core.Exceptions
{
    public class ValidationException : PokerBunchException
    {
        public IEnumerable<string> Messages { get; private set; }

        public ValidationException(string message)
        {
            Messages = new List<string> { message };
        }
    }
}