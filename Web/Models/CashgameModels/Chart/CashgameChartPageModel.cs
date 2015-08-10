using Core.UseCases;
using Newtonsoft.Json;
using Web.Models.PageBaseModels;

namespace Web.Models.CashgameModels.Chart
{
    public class CashgameChartPageModel : CashgamePageModel
    {
        public string ChartJson { get; private set; }

        public CashgameChartPageModel(CashgameContext.Result cashgameContextResult, CashgameChart.Result cashgameChartResult)
            : base("Cashgame Chart", cashgameContextResult)
        {
            ChartJson = JsonConvert.SerializeObject(new CashgameChartModel(cashgameChartResult));
        }
    }
}