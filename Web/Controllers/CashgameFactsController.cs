using System;
using System.Web.Mvc;
using Core.UseCases;
using Core.UseCases.CashgameFacts;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Facts;

namespace Web.Controllers
{
    public class CashgameFactsController : BaseController
    {
        [Authorize]
        [Route("{slug}/cashgame/facts/{year?}")]
        public ActionResult Facts(string slug, int? year = null)
        {
            var contextResult = GetCashgameContext(slug, DateTime.UtcNow, CashgameContext.CashgamePage.Facts, year);
            RequirePlayer(contextResult.BunchContext);
            var factsResult = UseCase.CashgameFacts.Execute(new CashgameFactsRequest(slug, year));

            var model = new CashgameFactsPageModel(contextResult, factsResult);
            return View("~/Views/Pages/CashgameFacts/FactsPage.cshtml", model);
        }
    }
}