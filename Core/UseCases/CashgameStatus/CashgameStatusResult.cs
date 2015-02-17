using Core.Urls;

namespace Core.UseCases.CashgameStatus
{
    public class CashgameStatusResult
    {
        public bool GameIsRunning { get; private set; }
        public Url RunningCashgameUrl { get; private set; }
        public Url AddCashgameUrl { get; private set; }

        public CashgameStatusResult(
            string slug,
            bool gameIsRunning)
        {
            GameIsRunning = gameIsRunning;
            RunningCashgameUrl = new RunningCashgameUrl(slug);
            AddCashgameUrl = new AddCashgameUrl(slug);
        }
    }
}