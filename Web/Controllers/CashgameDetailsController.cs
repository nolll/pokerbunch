using System.Web.Mvc;
using Core.UseCases.BunchContext;
using Core.UseCases.CashgameDetails;
using Web.Controllers.Base;
using Web.ModelFactories.CashgameModelFactories.Details;
using Web.Models.CashgameModels.Details;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class CashgameDetailsController : PokerBunchController
    {
        private readonly ICashgameDetailsChartJsonBuilder _cashgameDetailsChartJsonBuilder;

        public CashgameDetailsController(ICashgameDetailsChartJsonBuilder cashgameDetailsChartJsonBuilder)
        {
            _cashgameDetailsChartJsonBuilder = cashgameDetailsChartJsonBuilder;
        }

        [AuthorizePlayer]
        [Route("{slug}/cashgame/details/{dateStr}")]
        public ActionResult Details(string slug, string dateStr)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var cashgameDetailsResult = UseCase.CashgameDetails(new CashgameDetailsRequest(slug, dateStr));
            var model = new CashgameDetailsPageModel(contextResult, cashgameDetailsResult);
            return View("CashgameDetails/DetailsPage", model);
        }

        [AuthorizePlayer]
        [Route("{slug}/cashgame/detailschartjson/{dateStr}")]
        public ActionResult DetailsChartJson(string slug, string dateStr)
        {
            var model = _cashgameDetailsChartJsonBuilder.Build(slug, dateStr);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}