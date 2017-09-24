using System;
using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Routes;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Facts;

namespace Web.Controllers
{
    public class CashgameFactsController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Cashgame.Facts)]
        [Route(WebRoutes.Cashgame.FactsWithYear)]
        public ActionResult Facts(string slug, int? year = null)
        {
            var contextResult = GetCashgameContext(slug, DateTime.UtcNow, CashgameContext.CashgamePage.Facts, year);
            var factsResult = UseCase.CashgameFacts.Execute(new CashgameFacts.Request(slug, year));

            var model = new CashgameFactsPageModel(contextResult, factsResult);
            return View(model);
        }
    }
}