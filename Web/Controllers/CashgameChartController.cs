using System;
using System.Web.Mvc;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Chart;

namespace Web.Controllers
{
    public class CashgameChartController : BaseController
    {
        [Authorize]
        [Route(Routes.CashgameChart)]
        [Route(Routes.CashgameChartWithYear)]
        public ActionResult Chart(string slug, int? year = null)
        {
            var cashgameContextResult = GetCashgameContext(slug, DateTime.UtcNow, CashgameContext.CashgamePage.Chart, year);
            RequirePlayer(cashgameContextResult.BunchContext);
            var cashgameChartResult = UseCase.CashgameChart.Execute(new CashgameChart.Request(slug, year));
            var model = new CashgameChartPageModel(cashgameContextResult, cashgameChartResult);
            return View("~/Views/Pages/CashgameChart/Chart.cshtml", model);
        }
    }
}