using System.Web.Mvc;
using Core.UseCases.BunchContext;
using Core.UseCases.CashgameHome;
using Core.UseCases.CashgameTopList;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Index;

namespace Web.Controllers
{
    public class CashgameIndexController : PokerBunchController
    {
        [Authorize]
        [Route("{slug}/cashgame/index")]
        public ActionResult Index(string slug)
        {
            RequirePlayer(slug);
            var indexResult = UseCase.CashgameHome.Execute(new CashgameHomeRequest(slug));
            var contextResult = UseCase.BunchContext.Execute(new BunchContextRequest(slug));
            var topListResult = UseCase.TopList.Execute(new LatestTopListRequest(slug));
            var model = new CashgameIndexPageModel(contextResult, topListResult);
            return View("~/Views/Pages/CashgameIndex/CashgameIndex.cshtml", model);
        }
    }
}