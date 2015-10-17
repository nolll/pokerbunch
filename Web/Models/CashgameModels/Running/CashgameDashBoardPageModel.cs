using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Running
{
    public class CashgameDashBoardPageModel : RunningCashgamePageModel
    {
        public CashgameDashBoardPageModel(BunchContext.Result contextResult, RunningCashgame.Result runningCashgameResult)
            : base(contextResult, runningCashgameResult)
        {
        }

        public override bool IsInteractive
        {
            get { return false; }
        }

        public override string Layout
        {
            get { return ContextLayout.Wrapped; }
        }
    }
}