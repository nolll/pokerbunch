using System.Collections.Generic;

namespace Core.Exceptions
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