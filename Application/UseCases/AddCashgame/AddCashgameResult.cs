using Application.Urls;
using Application.UseCaseHelpers;

namespace Application.UseCases.AddCashgame
{
    public class AddCashgameResult : UseCaseResultWithValidator
    {
        public AddCashgameResult(string slug, Validator validator)
            : base(validator)
        {
            ReturnUrl = new RunningCashgameUrl(slug);
        }
    }
}