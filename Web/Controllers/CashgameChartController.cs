using System;
using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Chart;

namespace Web.Controllers
{
    public class CashgameChartController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Cashgame.Chart)]
        [Route(WebRoutes.Cashgame.ChartWithYear)]
        public ActionResult Chart(string slug, int? year = null)
        {
            var cashgameContextResult = GetCashgameContext(slug, DateTime.UtcNow, CashgameContext.CashgamePage.Chart, year);
            var cashgameChartResult = UseCase.CashgameChart.Execute(new CashgameChart.Request(Identity.UserName, slug, year));
            var model = new CashgameChartPageModel(cashgameContextResult, cashgameChartResult);
            return View("~/Views/Pages/CashgameChart/Chart.cshtml", model);
        }
    }
}