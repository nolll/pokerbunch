using System;
using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Index;
using Web.Routes;

namespace Web.Controllers
{
    public class CashgameIndexController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Cashgame.Index)]
        public ActionResult Index(string slug)
        {
            var contextResult = GetCashgameContext(slug, DateTime.UtcNow, CashgameContext.CashgamePage.Overview);
            var statusResult = UseCase.CashgameStatus.Execute(new CashgameStatus.Request(Identity.UserName, slug));
            var currentRankingsResult = UseCase.CurrentRankings.Execute(new CurrentRankings.Request(Identity.UserName, slug));
            var model = new CashgameIndexPageModel(contextResult, statusResult, currentRankingsResult);
            return View("~/Views/Pages/CashgameIndex/CashgameIndex.cshtml", model);
        }
    }
}