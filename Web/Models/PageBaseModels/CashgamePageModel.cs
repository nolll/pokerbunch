using Core.UseCases;
using Web.Components.NavigationModels;
using Web.Models.NavigationModels;

namespace Web.Models.PageBaseModels
{
    public abstract class CashgamePageModel : BunchPageModel
    {
        public CashgamePageNavigationModel PageNavModel { get; private set; }
        public CashgameYearNavigationModel YearNavModel { get; private set; }

        protected CashgamePageModel(string browserTitle, CashgameContext.Result cashgameContextResult) : base(browserTitle, cashgameContextResult.BunchContext)
        {
            PageNavModel = new CashgamePageNavigationModel(cashgameContextResult);
            YearNavModel = cashgameContextResult.SelectedPage != CashgameContext.CashgamePage.Overview ? new CashgameYearNavigationModel(cashgameContextResult) : null;
        }

        public override string Layout
        {
            get { return ContextLayout.Cashgame; }
        }
    }
}