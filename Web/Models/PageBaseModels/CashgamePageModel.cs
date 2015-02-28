using Core.UseCases.CashgameContext;
using Web.Models.NavigationModels;

namespace Web.Models.PageBaseModels
{
    public abstract class CashgamePageModel : BunchPageModel
    {
        public CashgamePageNavigationModel PageNavModel { get; private set; }
        public CashgameYearNavigationModel YearNavModel { get; private set; }

        protected CashgamePageModel(string browserTitle, CashgameContextResult cashgameContextResult) : base(browserTitle, cashgameContextResult.BunchContext)
        {
            PageNavModel = new CashgamePageNavigationModel(cashgameContextResult);
            YearNavModel = cashgameContextResult.SelectedPage != CashgamePage.Overview ? new CashgameYearNavigationModel(cashgameContextResult) : null;
        }

        public override string Layout
        {
            get { return ContextLayout.Cashgame; }
        }
    }
}