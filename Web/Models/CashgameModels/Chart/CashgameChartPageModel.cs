using Application.Urls;
using Application.UseCases.CashgameContext;
using Web.Models.NavigationModels;

namespace Web.Models.CashgameModels.Chart
{
    public class CashgameChartPageModel : CashgameContextPageModel
    {
        public Url ChartDataUrl { get; set; }

        public CashgameChartPageModel(CashgameContextResult cashgameContextResult)
            : base("Cashgame Chart", cashgameContextResult, CashgamePage.Chart)
        {
        }
    }
}