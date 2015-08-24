using Core.Urls;
using Core.UseCases;
using Web.Models.PageBaseModels;
using Web.Urls;

namespace Web.Models.CashgameModels.Running
{
    public class RunningCashgamePageModel : BunchPageModel
    {
        public string GameDataUrl { get; private set; }

	    public RunningCashgamePageModel(BunchContext.Result contextResult, RunningCashgame.Result runningCashgameResult)
            : base("Running Cashgame", contextResult)
	    {
	        GameDataUrl = new RunningCashgameGameJsonUrl(runningCashgameResult.Slug).Relative;
	    }
    }
}