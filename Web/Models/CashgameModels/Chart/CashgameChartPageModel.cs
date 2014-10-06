using Core.UseCases.CashgameChartContainer;
using Core.UseCases.CashgameContext;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Chart
{
    public class CashgameChartPageModel : CashgamePageModel
    {
        public string ChartDataUrl { get; private set; }

        public CashgameChartPageModel(CashgameContextResult cashgameContextResult, CashgameChartContainerResult cashgameChartContainerResult)
            : base("Cashgame Chart", cashgameContextResult)
        {
            ChartDataUrl = cashgameChartContainerResult.DataUrl.Relative;
        }
    }
}