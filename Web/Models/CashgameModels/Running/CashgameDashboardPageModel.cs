using Core.UseCases;
using Web.Extensions;
using Web.Models.MiscModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Running
{
    public class CashgameDashboardPageModel : PageModel, IRunningCashgamePageModel
    {
        public string Slug { get; }
        public string ApiHost => SiteSettings.ApiHost;

        public SpinnerModel SpinnerModel => new SpinnerModel();

        public CashgameDashboardPageModel(BaseContext.Result contextResult, RunningCashgame.Result runningCashgameResult)
            : base(contextResult)
        {
            Slug = runningCashgameResult.Slug;
        }

        public override string Layout => ContextLayout.Base;
        public override string BrowserTitle => "Cashgame Dashboard";

        public override View GetView()
        {
            return new View("~/Views/Pages/RunningCashgame/DashboardPage.cshtml");
        }
    }
}