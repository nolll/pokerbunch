using System;
using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Index;

namespace Web.Controllers
{
    public class CashgameIndexController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Cashgame.Index)]
        public ActionResult Index(string slug)
        {
            var contextResult = GetCashgameContext(slug, DateTime.UtcNow, CashgameContext.CashgamePage.Overview);
            var statusResult = UseCase.CashgameStatus.Execute(new CashgameStatus.Request(CurrentUserName, slug));
            var currentRankingsResult = UseCase.CurrentRankings.Execute(new CurrentRankings.Request(CurrentUserName, slug));
            var model = new CashgameIndexPageModel(contextResult, statusResult, currentRankingsResult);
            return View("~/Views/Pages/CashgameIndex/CashgameIndex.cshtml", model);
        }
    }
}