using Core.Urls;

namespace Core.UseCases.EditCashgame
{
    public class EditCashgameResult
    {
        public Url ReturnUrl { get; private set; }

        public EditCashgameResult(Url returnUrl)
        {
            ReturnUrl = returnUrl;
        }
    }
}