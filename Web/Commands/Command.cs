using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Commands
{
    public abstract class Command
    {
        public List<ValidationResult> Errors { get; private set; }

        protected bool IsValid(object model)
        {
            var context = new ValidationContext(model, null, null);
            if (!Validator.TryValidateObject(model, context, Errors))
            {
                return false;
            }
            return true;
        }

        public abstract bool Execute();
    }
}