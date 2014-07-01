using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Application
{
    public class Validator
    {
        private readonly object _subject;
        private readonly IList<ValidationResult> _errors;

        public Validator(object subject)
        {
            _subject = subject;
            _errors = new List<ValidationResult>();
            var context = new ValidationContext(_subject);
            System.ComponentModel.DataAnnotations.Validator.TryValidateObject(_subject, context, _errors, true);
        }

        public void AddError(string message)
        {
            _errors.Add(new ValidationResult(message));
        }

        public IEnumerable<string> Errors
        {
            get { return _errors.Select(o => o.ErrorMessage); }
        }

        public bool HasErrors
        {
            get { return _errors.Count > 0; }
        }

        public bool IsValid
        {
            get { return !HasErrors; }
        }
    }
}