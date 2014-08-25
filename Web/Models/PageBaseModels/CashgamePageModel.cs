using Application.UseCases.CashgameContext;
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
            var slug = cashgameContextResult.BunchContext.Slug;
            BarModel = cashgameContextResult.GameIsRunning ? new RunningGameBarModel(slug) : new BarModel(slug);
            StartButtonModel = cashgameContextResult.GameIsRunning ? new RunningGameStartButtonModel(slug) : new StartButtonModel(slug);
            PageNavModel = new CashgamePageNavigationModel(cashgameContextResult);
            YearNavModel = new CashgameYearNavigationModel(cashgameContextResult);
        }

        public override string Layout
        {
            get { return ContextLayout.Cashgame; }
        }
    }
}