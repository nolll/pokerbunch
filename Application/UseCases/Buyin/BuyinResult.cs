using Application.Urls;
using Application.UseCaseHelpers;

namespace Application.UseCases.Buyin
{
    public class BuyinResult : UseCaseResultWithValidator
    {
        public BuyinResult(string slug, Validator validator)
            : base(validator)
        {
            ReturnUrl = new RunningCashgameUrl(slug);
        }
    }
}