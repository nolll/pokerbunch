using Core.Urls;

namespace Core.UseCases.Buyin
{
    public class BuyinResult
    {
        public Url ReturnUrl { get; private set; }

        public BuyinResult(string slug)
        {
            ReturnUrl = new RunningCashgameUrl(slug);
        }
    }
}