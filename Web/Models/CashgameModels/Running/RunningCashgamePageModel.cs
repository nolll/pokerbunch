using Core.UseCases;
using Web.Extensions;
using Web.Models.MiscModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Running
{
    public class RunningCashgamePageModel : BunchPageModel, IRunningCashgamePageModel
    {
        public string Slug { get; }
        public string ApiHost => SiteSettings.ApiHost;
        public SpinnerModel SpinnerModel => new SpinnerModel();

        public RunningCashgamePageModel(BunchContext.Result contextResult, RunningCashgame.Result runningCashgameResult)
            : base(contextResult)
        {
            Slug = runningCashgameResult.Slug;
        }

        public override string BrowserTitle => "Running Cashgame";

        public override View GetView()
        {
            return new View("~/Views/Pages/RunningCashgame/RunningPage.cshtml");
        }
    }
}