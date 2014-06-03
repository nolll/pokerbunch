using Application.UseCases.CashgameContext;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels
{
    public abstract class CashgameContextPageModel : PageModel
    {
        public CashgamePageNavigationModel PageNavModel { get; private set; }
        public CashgameYearNavigationModel YearNavModel { get; private set; }

        protected CashgameContextPageModel(
            string browserTitle,
            CashgameContextResult cashgameContextResult,
            CashgamePage selectedPage)
            : base(browserTitle, cashgameContextResult)
        {
            PageNavModel = new CashgamePageNavigationModel(cashgameContextResult, selectedPage);
            YearNavModel = new CashgameYearNavigationModel(cashgameContextResult, selectedPage);;
        }
    }
}