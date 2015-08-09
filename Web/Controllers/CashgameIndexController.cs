using System;
using System.Web.Mvc;
using Core.UseCases;
using Core.UseCases.CashgameCurrentRankings;
using Core.UseCases.CashgameStatus;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Index;

namespace Web.Controllers
{
    public class CashgameIndexController : BaseController
    {
        [Authorize]
        [Route("{slug}/cashgame")]
        [Route("{slug}/cashgame/index")]
        public ActionResult Index(string slug)
        {
            var contextResult = GetCashgameContext(slug, DateTime.UtcNow, CashgameContext.CashgamePage.Overview);
            RequirePlayer(contextResult.BunchContext);
            var statusResult = UseCase.CashgameStatus.Execute(new CashgameStatusRequest(slug));
            var currentRankingsResult = UseCase.CurrentRankings.Execute(new CurrentRankingsRequest(slug));
            var model = new CashgameIndexPageModel(contextResult, statusResult, currentRankingsResult);
            return View("~/Views/Pages/CashgameIndex/CashgameIndex.cshtml", model);
        }
    }
}