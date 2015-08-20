using Core.UseCases;
using Web.Urls;

namespace Web.Models.CashgameModels.Status
{
    public class CashgameStatusModel
    {
        public string Heading { get; private set; }
        public string LinkText { get; private set; }
        public string Description { get; private set; }
        public string Url { get; private set; }

        public CashgameStatusModel(CashgameStatus.Result statusResult)
        {
            Heading = "Current Game";
            LinkText = statusResult.GameIsRunning ? "Go to game" : "Start a game";
            Description = statusResult.GameIsRunning ? "There is a game running" : "No game is running at the moment";
            Url = statusResult.GameIsRunning ? new RunningCashgameUrl(statusResult.Slug).Relative : new AddCashgameUrl(statusResult.Slug).Relative;
        }
    }
}