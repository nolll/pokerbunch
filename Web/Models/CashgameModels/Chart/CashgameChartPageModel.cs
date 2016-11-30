using Core.UseCases;
using Web.Models.PageBaseModels;
using Web.Services;

namespace Web.Models.CashgameModels.Chart
{
    public class CashgameChartPageModel : CashgamePageModel
    {
        public string ChartJson { get; private set; }

        public CashgameChartPageModel(CashgameContext.Result cashgameContextResult, CashgameChart.Result cashgameChartResult)
            : base(cashgameContextResult)
        {
            ChartJson = JsonHelper.Serialize(new CashgameChartModel(cashgameChartResult));
        }

        public override string BrowserTitle => "Cashgame Chart";
    }
}