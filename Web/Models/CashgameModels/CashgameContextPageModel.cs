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
            PageProperties pageProperties,
            CashgameContextResult contextResult,
            CashgamePage selectedPage)
            : base(browserTitle, pageProperties)
        {
            PageNavModel = new CashgamePageNavigationModel(contextResult, selectedPage);
            YearNavModel = new CashgameYearNavigationModel(contextResult, selectedPage);;
        }
    }
}