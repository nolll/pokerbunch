using System;
using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Chart;

namespace Web.Controllers
{
    public class CashgameChartController : BaseController
    {
        [Authorize]
        [Route(ChartUrl.Route)]
        [Route(ChartUrl.RouteWithYear)]
        public ActionResult Chart(string bunchId, int? year = null)
        {
            var cashgameContextResult = GetCashgameContext(bunchId, DateTime.UtcNow, CashgameContext.CashgamePage.Chart, year);
            var cashgameChartResult = UseCase.CashgameChart.Execute(new CashgameChart.Request(bunchId, year));
            var model = new CashgameChartPageModel(cashgameContextResult, cashgameChartResult);
            return View(model);
        }
    }
}