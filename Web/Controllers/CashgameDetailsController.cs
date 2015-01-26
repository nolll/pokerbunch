using System;
using System.Web.Mvc;
using Core.UseCases.BunchContext;
using Core.UseCases.CashgameDetails;
using Core.UseCases.CashgameDetailsChart;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Details;
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
            var cashgameDetailsChartResult = UseCase.CashgameDetailsChart(new CashgameDetailsChartRequest(slug, DateTime.UtcNow, dateStr));
            var model = new CashgameDetailsPageModel(contextResult, cashgameDetailsResult, cashgameDetailsChartResult);
            return View("~/Views/Pages/CashgameDetails/DetailsPage.cshtml", model);
        }
    }
}