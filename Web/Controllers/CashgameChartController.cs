using System.Web.Mvc;
using Core.UseCases.CashgameChartContainer;
using Core.UseCases.CashgameContext;
using Web.Controllers.Base;
using Web.ModelFactories.CashgameModelFactories.Chart;
using Web.Models.CashgameModels.Chart;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class CashgameChartController : PokerBunchController
    {
        private readonly ICashgameSuiteChartJsonBuilder _cashgameSuiteChartJsonBuilder;

        public CashgameChartController(ICashgameSuiteChartJsonBuilder cashgameSuiteChartJsonBuilder)
        {
            _cashgameSuiteChartJsonBuilder = cashgameSuiteChartJsonBuilder;
        }

        [AuthorizePlayer]
        [Route("{slug}/cashgame/chart/{year?}")]
        public ActionResult Chart(string slug, int? year = null)
        {
            var cashgameContextResult = UseCase.CashgameContext(new CashgameContextRequest(slug, year, CashgamePage.Chart));
            var cashgameChartContainerResult = UseCase.CashgameChartContainer(new CashgameChartContainerRequest(slug, year));
            var model = new CashgameChartPageModel(cashgameContextResult, cashgameChartContainerResult);
            return View("~/Views/Pages/CashgameChart/Chart.cshtml", model);
        }

        [AuthorizePlayer]
        [Route("{slug}/cashgame/chartjson/{year?}")]
        public JsonResult ChartJson(string slug, int? year = null)
        {
            var model = _cashgameSuiteChartJsonBuilder.Build(slug, year);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}