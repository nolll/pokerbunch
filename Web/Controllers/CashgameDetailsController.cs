using System.Web.Mvc;
using Core.UseCases.BunchContext;
using Core.UseCases.CashgameDetails;
using Core.UseCases.CashgameDetailsChart;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Details;
using Web.Plumbing;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class CashgameDetailsController : PokerBunchController
    {
        [AuthorizePlayer]
        [Route("{slug}/cashgame/details/{dateStr}")]
        public ActionResult Details(string slug, string dateStr)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var cashgameDetailsResult = UseCase.CashgameDetails(new CashgameDetailsRequest(slug, dateStr));
            var model = new CashgameDetailsPageModel(contextResult, cashgameDetailsResult);
            return View("~/Views/Pages/CashgameDetails/DetailsPage.cshtml", model);
        }

        [AuthorizePlayer]
        [Route("{slug}/cashgame/detailschartjson/{dateStr}")]
        public ActionResult DetailsChartJson(string slug, string dateStr)
        {
            var cashgameDetailsChartResult = UseCaseContainer.Instance.CashgameDetailsChart(new CashgameDetailsChartRequest(slug, dateStr));
            var model = new DetailsChartModel(cashgameDetailsChartResult);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}