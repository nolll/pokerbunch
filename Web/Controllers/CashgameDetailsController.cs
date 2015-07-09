using System;
using System.Web.Mvc;
using Core.UseCases.CashgameDetails;
using Core.UseCases.CashgameDetailsChart;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Details;

namespace Web.Controllers
{
    public class CashgameDetailsController : PokerBunchController
    {
        [Authorize]
        [Route("{slug}/cashgame/details/{dateStr}")]
        public ActionResult Details(string slug, string dateStr)
        {
            RequirePlayer(slug);
            var contextResult = GetBunchContext(slug);
            var cashgameDetailsResult = UseCase.CashgameDetails.Execute(new CashgameDetailsRequest(slug, dateStr));
            var cashgameDetailsChartResult = UseCase.CashgameDetailsChart.Execute(new CashgameDetailsChartRequest(slug, DateTime.UtcNow, dateStr));
            var model = new CashgameDetailsPageModel(contextResult, cashgameDetailsResult, cashgameDetailsChartResult);
            return View("~/Views/Pages/CashgameDetails/DetailsPage.cshtml", model);
        }
    }
}