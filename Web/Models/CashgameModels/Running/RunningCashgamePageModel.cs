using Core.UseCases;
using Web.Extensions;
using Web.Models.MiscModels;
using Web.Models.PageBaseModels;
using Web.Urls.SiteUrls;

namespace Web.Models.CashgameModels.Running
{
    public interface IRunningCashgamePageModel
    {
        string GameDataUrl { get; }
        SpinnerModel SpinnerModel { get; }
    }

    public class RunningCashgamePageModel : BunchPageModel, IRunningCashgamePageModel
    {
        public string GameDataUrl { get; }
        public SpinnerModel SpinnerModel => new SpinnerModel();

        public RunningCashgamePageModel(BunchContext.Result contextResult, RunningCashgame.Result runningCashgameResult)
            : base(contextResult)
        {
            GameDataUrl = new RunningCashgameGameJsonUrl(runningCashgameResult.Slug).Relative;
        }

        public override string BrowserTitle => "Running Cashgame";

        public override View GetView()
        {
            return new View("~/Views/Pages/RunningCashgame/RunningPage.cshtml");
        }
    }
}