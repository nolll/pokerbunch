using Core.UseCases;
using Web.Extensions;
using Web.Models.MiscModels;
using Web.Models.PageBaseModels;
using Web.Services;

namespace Web.Models.CashgameModels.Chart
{
    public class CashgameChartPageModel : CashgamePageModel
    {
        public string ChartJson { get; private set; }
        public SpinnerModel SpinnerModel => new SpinnerModel();

        public CashgameChartPageModel(CashgameContext.Result cashgameContextResult, CashgameChart.Result cashgameChartResult)
            : base(cashgameContextResult)
        {
            ChartJson = JsonHelper.Serialize(new CashgameChartModel(cashgameChartResult));
        }

        public override string BrowserTitle => "Cashgame Chart";

        public override View GetView()
        {
            return new View("~/Views/Pages/CashgameChart/Chart.cshtml");
        }
    }
}