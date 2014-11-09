using System.Web.Mvc;
using Core.UseCases.CashgameChart;
using Core.UseCases.CashgameContext;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Chart;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class CashgameChartController : PokerBunchController
    {
        [AuthorizePlayer]
        [Route("{slug}/cashgame/chart/{year?}")]
        public ActionResult Chart(string slug, int? year = null)
        {
            var cashgameContextResult = UseCase.CashgameContext(new CashgameContextRequest(slug, year, CashgamePage.Chart));
            var cashgameChartResult = UseCase.CashgameChart(new CashgameChartRequest(slug, year));
            var model = new CashgameChartPageModel(cashgameContextResult, cashgameChartResult);
            return View("~/Views/Pages/CashgameChart/Chart.cshtml", model);
        }
    }
}