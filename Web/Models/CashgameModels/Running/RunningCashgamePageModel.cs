using Core.UseCases;
using Web.Common.Urls.SiteUrls;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Running
{
    public interface IRunningCashgamePageModel
    {
        string GameDataUrl { get; }
    }

    public class RunningCashgamePageModel : BunchPageModel, IRunningCashgamePageModel
    {
        public string GameDataUrl { get; }

        public RunningCashgamePageModel(BunchContext.Result contextResult, RunningCashgame.Result runningCashgameResult)
            : base(contextResult)
        {
            GameDataUrl = new RunningCashgameGameJsonUrl(runningCashgameResult.Slug).Relative;
        }

        public override string BrowserTitle => "Running Cashgame";
    }
}