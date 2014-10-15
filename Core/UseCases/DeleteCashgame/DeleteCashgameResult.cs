using Core.Urls;

namespace Core.UseCases.DeleteCashgame
{
    public class DeleteCashgameResult
    {
        public Url ReturnUrl { get; private set; }

        public DeleteCashgameResult(Url returnUrl)
        {
            ReturnUrl = returnUrl;
        }
    }
}