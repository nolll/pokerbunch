using Core.UseCases.CashgameContext;
using Web.Models.CashgameModels.Running;
using Web.Models.NavigationModels;

namespace Web.Models.PageBaseModels
{
    public abstract class CashgamePageModel : BunchPageModel
    {
        public BarModel BarModel { get; private set; }
        public StartButtonModel StartButtonModel { get; private set; }
        public CashgamePageNavigationModel PageNavModel { get; private set; }
        public CashgameYearNavigationModel YearNavModel { get; private set; }

        protected CashgamePageModel(string browserTitle, CashgameContextResult cashgameContextResult) : base(browserTitle, cashgameContextResult.BunchContext)
        {
            BarModel = cashgameContextResult.GameIsRunning ? new RunningGameBarModel(cashgameContextResult.RunningCashgameUrl.Relative) : new BarModel(cashgameContextResult.AddCashgameUrl.Relative);
            StartButtonModel = cashgameContextResult.GameIsRunning ? new RunningGameStartButtonModel(cashgameContextResult.AddCashgameUrl.Relative) : new StartButtonModel(cashgameContextResult.AddCashgameUrl.Relative);
            PageNavModel = new CashgamePageNavigationModel(cashgameContextResult);
            YearNavModel = new CashgameYearNavigationModel(cashgameContextResult);
        }

        public override string Layout
        {
            get { return ContextLayout.Cashgame; }
        }
    }
}