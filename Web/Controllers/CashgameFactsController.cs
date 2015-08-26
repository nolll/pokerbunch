using System;
using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Facts;
using Web.Urls;

namespace Web.Controllers
{
    public class CashgameFactsController : BaseController
    {
        [Authorize]
        [Route(Routes.CashgameFacts)]
        [Route(Routes.CashgameFactsWithYear)]
        public ActionResult Facts(string slug, int? year = null)
        {
            var contextResult = GetCashgameContext(slug, DateTime.UtcNow, CashgameContext.CashgamePage.Facts, year);
            var factsResult = UseCase.CashgameFacts.Execute(new CashgameFacts.Request(CurrentUserName, slug, year));

            var model = new CashgameFactsPageModel(contextResult, factsResult);
            return View("~/Views/Pages/CashgameFacts/FactsPage.cshtml", model);
        }
    }
}