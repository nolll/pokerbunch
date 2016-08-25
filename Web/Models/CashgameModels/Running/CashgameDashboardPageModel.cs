using Core.UseCases;
using Web.Common.Urls.SiteUrls;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Running
{
    public class CashgameDashboardPageModel : PageModel, IRunningCashgamePageModel
    {
        public string GameDataUrl { get; }

        public CashgameDashboardPageModel(BaseContext.Result contextResult, RunningCashgame.Result runningCashgameResult)
            : base(contextResult)
        {
            GameDataUrl = new RunningCashgameGameJsonUrl(runningCashgameResult.Slug).Relative;
        }

        public override string Layout => ContextLayout.Base;
        public override string BrowserTitle => "Cashgame Dashboard";
    }
}