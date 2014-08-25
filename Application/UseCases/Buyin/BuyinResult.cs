using System.Collections.Generic;
using Application.Urls;

namespace Application.UseCases.Buyin
{
    public class BuyinResult
    {
        public bool Success { get; private set; }
        public IEnumerable<string> Errors { get; private set; }
        public Url ReturnUrl { get; private set; }

        public BuyinResult(string slug, Validator validator)
        {
            Success = validator.IsValid;
            Errors = validator.Errors;
            ReturnUrl = new RunningCashgameUrl(slug);
        }
    }
}