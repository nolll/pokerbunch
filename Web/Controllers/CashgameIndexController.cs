using System;
using System.Web.Mvc;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Index;

namespace Web.Controllers
{
    public class CashgameIndexController : BaseController
    {
        [Authorize]
        [Route(Routes.CashgameIndex)]
        public ActionResult Index(string slug)
        {
            var contextResult = GetCashgameContext(slug, DateTime.UtcNow, CashgameContext.CashgamePage.Overview);
            RequirePlayer(contextResult.BunchContext);
            var statusResult = UseCase.CashgameStatus.Execute(new CashgameStatus.Request(slug));
            var currentRankingsResult = UseCase.CurrentRankings.Execute(new CurrentRankings.Request(slug));
            var model = new CashgameIndexPageModel(contextResult, statusResult, currentRankingsResult);
            return View("~/Views/Pages/CashgameIndex/CashgameIndex.cshtml", model);
        }
    }
}