using System.Collections.Generic;
using Application.Urls;

namespace Application.UseCaseHelpers
{
    public abstract class UseCaseResultWithValidator
    {
        public bool Success { get; private set; }
        public IEnumerable<string> Errors { get; private set; }
        public Url ReturnUrl { get; protected set; }

        protected UseCaseResultWithValidator(Validator validator)
        {
            Success = validator.IsValid;
            Errors = validator.Errors;
            ReturnUrl = Url.Empty;
        }
    }
}