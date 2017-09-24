using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;
using Web.Models.MiscModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Running
{
    public class CashgameDashboardPageModel : PageModel, IRunningCashgamePageModel
    {
        public string GameDataUrl { get; }
        public SpinnerModel SpinnerModel => new SpinnerModel();

        public CashgameDashboardPageModel(BaseContext.Result contextResult, RunningCashgame.Result runningCashgameResult)
            : base(contextResult)
        {
            GameDataUrl = new RunningCashgameGameJsonUrl(runningCashgameResult.Slug).Relative;
        }

        public override string Layout => ContextLayout.Base;
        public override string BrowserTitle => "Cashgame Dashboard";

        public override View GetView()
        {
            return new View("~/Views/Pages/RunningCashgame/DashboardPage.cshtml");
        }
    }
}