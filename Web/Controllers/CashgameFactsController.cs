using System.Web.Mvc;
using Core.UseCases.CashgameContext;
using Core.UseCases.CashgameFacts;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Facts;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class CashgameFactsController : PokerBunchController
    {
        [AuthorizePlayer]
        [Route("{slug}/cashgame/facts/{year?}")]
        public ActionResult Facts(string slug, int? year = null)
        {
            var contextResult = UseCase.CashgameContext(new CashgameContextRequest(slug, year, CashgamePage.Facts));
            var factsResult = UseCase.CashgameFacts(new CashgameFactsRequest(slug, year));

            var model = new CashgameFactsPageModel(contextResult, factsResult);
            return View("CashgameFacts/FactsPage", model);
        }
    }
}