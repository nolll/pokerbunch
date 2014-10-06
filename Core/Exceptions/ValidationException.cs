using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Exceptions
{
    public class ValidationException : Exception
    {
        public IEnumerable<string> Messages { get; private set; }

        public ValidationException(Validator validator)
        {
            Messages = validator.Errors.ToList();
        }
    }
}