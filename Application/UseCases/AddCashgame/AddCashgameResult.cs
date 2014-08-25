using System.Collections.Generic;
using Application.Urls;

namespace Application.UseCases.AddCashgame
{
    public class AddCashgameResult
    {
        public bool Success { get; private set; }
        public IEnumerable<string> Errors { get; private set; }
        public Url ReturnUrl { get; private set; }

        public AddCashgameResult(string slug, Validator validator)
        {
            Success = validator.IsValid;
            Errors = validator.Errors;
            ReturnUrl = new RunningCashgameUrl(slug);
        }
    }
}