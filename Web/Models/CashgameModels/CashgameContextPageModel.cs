using Application.UseCases.CashgameContext;
using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels
{
    public abstract class CashgameContextPageModel : BunchPageModel
    {
        public CashgamePageNavigationModel PageNavModel { get; private set; }
        public CashgameYearNavigationModel YearNavModel { get; private set; }

        protected CashgameContextPageModel(
            string browserTitle,
            CashgameContextResult cashgameContextResult)
            : base(browserTitle, cashgameContextResult.BunchContext)
        {
            PageNavModel = new CashgamePageNavigationModel(cashgameContextResult);
            YearNavModel = new CashgameYearNavigationModel(cashgameContextResult);;
        }
    }
}