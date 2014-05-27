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
            CashgamePageNavigationModel pageNavModel,
            CashgameYearNavigationModel yearNavModel)
            : base(browserTitle, pageProperties)
        {
            PageNavModel = pageNavModel;
            YearNavModel = yearNavModel;
        }
    }
}