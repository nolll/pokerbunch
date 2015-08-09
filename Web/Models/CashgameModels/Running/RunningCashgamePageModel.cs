using Core.UseCases;
using Core.UseCases.RunningCashgame;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Running
{
    public class RunningCashgamePageModel : BunchPageModel
    {
        public string GameDataUrl { get; private set; }

	    public RunningCashgamePageModel(BunchContext.Result contextResult, RunningCashgameResult runningCashgameResult)
            : base("Running Cashgame", contextResult)
	    {
	        GameDataUrl = runningCashgameResult.GameDataUrl.Relative;
	    }
    }
}