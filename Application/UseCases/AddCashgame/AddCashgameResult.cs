using Application.Urls;

namespace Application.UseCases.AddCashgame
{
    public class AddCashgameResult
    {
        public Url ReturnUrl { get; private set; }

        public AddCashgameResult(string slug)
        {
            ReturnUrl = new RunningCashgameUrl(slug);
        }
    }
}