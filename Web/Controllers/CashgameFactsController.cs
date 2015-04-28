using System;
using System.Web.Mvc;
using Core.UseCases.CashgameContext;
using Core.UseCases.CashgameFacts;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Facts;

namespace Web.Controllers
{
    public class CashgameFactsController : PokerBunchController
    {
        [Authorize]
        [Route("{slug}/cashgame/facts/{year?}")]
        public ActionResult Facts(string slug, int? year = null)
        {
            RequirePlayer(slug);
            var contextResult = UseCase.CashgameContext.Execute(new CashgameContextRequest(slug, DateTime.UtcNow, CashgamePage.Facts, year));
            var factsResult = UseCase.CashgameFacts.Execute(new CashgameFactsRequest(slug, year));

            var model = new CashgameFactsPageModel(contextResult, factsResult);
            return View("~/Views/Pages/CashgameFacts/FactsPage.cshtml", model);
        }
    }
}