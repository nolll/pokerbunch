using Application.Urls;
using Application.UseCases.CashgameContext;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Chart
{
    public class CashgameChartPageModel : CashgamePageModel
    {
        public Url ChartDataUrl { get; set; }

        public CashgameChartPageModel(CashgameContextResult cashgameContextResult)
            : base("Cashgame Chart", cashgameContextResult)
        {
        }
    }
}