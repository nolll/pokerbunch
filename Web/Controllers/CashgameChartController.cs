using System;
using System.Web.Mvc;
using Core.UseCases.CashgameChart;
using Core.UseCases.CashgameContext;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Chart;

namespace Web.Controllers
{
    public class CashgameChartController : PokerBunchController
    {
        [Authorize]
        [Route("{slug}/cashgame/chart/{year?}")]
        public ActionResult Chart(string slug, int? year = null)
        {
            RequirePlayer(slug);
            var cashgameContextResult = GetCashgameContext(slug, DateTime.UtcNow, CashgamePage.Chart, year);
            var cashgameChartResult = UseCase.CashgameChart.Execute(new CashgameChartRequest(slug, year));
            var model = new CashgameChartPageModel(cashgameContextResult, cashgameChartResult);
            return View("~/Views/Pages/CashgameChart/Chart.cshtml", model);
        }
    }
}