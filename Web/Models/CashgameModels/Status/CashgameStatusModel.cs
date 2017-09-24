using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;

namespace Web.Models.CashgameModels.Status
{
    public class CashgameStatusModel : IViewModel
    {
        public string Heading { get; }
        public string LinkText { get; }
        public string Description { get; }
        public string Url { get; }
        public string DashboardUrl { get; }
        public bool GameIsRunning { get; }

        public CashgameStatusModel(CashgameStatus.Result statusResult)
        {
            Heading = "Current Game";
            LinkText = statusResult.GameIsRunning ? "Go to game" : "Start a game";
            Description = statusResult.GameIsRunning ? "There is a game running" : "No game is running at the moment";
            Url = statusResult.GameIsRunning ? new RunningCashgameUrl(statusResult.Slug).Relative : new AddCashgameUrl(statusResult.Slug).Relative;
            DashboardUrl = new DashboardCashgameUrl(statusResult.Slug).Relative;
            GameIsRunning = statusResult.GameIsRunning;
        }

        public View GetView()
        {
            return new View("CashgameIndex/Status");
        }
    }
}