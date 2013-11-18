using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Web.Commands
{
    public abstract class Command
    {
        private readonly IList<ValidationResult> _errors;

        protected Command()
        {
            _errors = new List<ValidationResult>();
        }

        protected bool IsValid(object model)
        {
            var context = new ValidationContext(model, null, null);
            if (!Validator.TryValidateObject(model, context, _errors))
            {
                return false;
            }
            return true;
        }

        protected void AddError(string message)
        {
            _errors.Add(new ValidationResult(message));
        }

        public IEnumerable<string> Errors
        {
            get { return _errors.Select(o => o.ErrorMessage); }
        } 

        protected bool HasErrors
        {
            get { return _errors.Count > 0; }
        }

        public abstract bool Execute();
    }
}