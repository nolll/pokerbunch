using Core.UseCases;
using Web.Components.NavigationModels;
using Web.Models.NavigationModels;

namespace Web.Models.PageBaseModels
{
    public abstract class CashgamePageModel : BunchPageModel
    {
        public CashgamePageNavigationModel PageNavModel { get; private set; }
        public CashgameYearNavigationModel YearNavModel { get; private set; }

        protected CashgamePageModel(CashgameContext.Result cashgameContextResult)
            : base(cashgameContextResult.BunchContext)
        {
            PageNavModel = new CashgamePageNavigationModel(cashgameContextResult);
            YearNavModel = cashgameContextResult.SelectedPage != CashgameContext.CashgamePage.Overview ? new CashgameYearNavigationModel(cashgameContextResult) : null;
        }

        public override string Layout => ContextLayout.Cashgame;
    }
}