using Core.UseCases;
using Web.Common.Urls.SiteUrls;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Running
{
    public class CashgameDashboardPageModel : PageModel, IRunningCashgamePageModel
    {
        public string GameDataUrl { get; private set; }

        public CashgameDashboardPageModel(BaseContext.Result contextResult, RunningCashgame.Result runningCashgameResult)
            : base("Cashgame DashBoard", contextResult)
        {
            GameDataUrl = new RunningCashgameGameJsonUrl(runningCashgameResult.Slug).Relative;
        }

        public override string Layout
        {
            get { return ContextLayout.Base; }
        }
    }
}