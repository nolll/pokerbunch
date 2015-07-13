using System;
using System.Web.Mvc;
using Core.UseCases.CashgameContext;
using Core.UseCases.CashgameStatus;
using Core.UseCases.CashgameTopList;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Index;

namespace Web.Controllers
{
    public class CashgameIndexController : PokerBunchController
    {
        [Authorize]
        [Route("{slug}/cashgame")]
        [Route("{slug}/cashgame/index")]
        public ActionResult Index(string slug)
        {
            var contextResult = GetCashgameContext(slug, DateTime.UtcNow, CashgamePage.Overview);
            RequirePlayer(contextResult.BunchContext);
            var statusResult = UseCase.CashgameStatus.Execute(new CashgameStatusRequest(slug));
            var topListResult = UseCase.TopList.Execute(new LatestTopListRequest(slug));
            var model = new CashgameIndexPageModel(contextResult, statusResult, topListResult);
            return View("~/Views/Pages/CashgameIndex/CashgameIndex.cshtml", model);
        }
    }
}