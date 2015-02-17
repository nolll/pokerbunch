using System.Web.Mvc;
using Core.UseCases.CashgameContext;
using Core.UseCases.CashgameTopList;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Toplist;

namespace Web.Controllers
{
    public class TopListController : PokerBunchController
    {
        [Authorize]
        [Route("{slug}/cashgame/toplist/{year?}")]
        public ActionResult Toplist(string slug, string orderBy = null, int? year = null)
        {
            RequirePlayer(slug);
            var contextResult = UseCase.CashgameContext.Execute(new CashgameContextRequest(slug, CashgamePage.Toplist, year));
            var topListResult = UseCase.TopList.Execute(new TopListRequest(slug, orderBy, year));
            var model = new CashgameToplistPageModel(contextResult, topListResult);
            return View("~/Views/Pages/Toplist/ToplistPage.cshtml", model);
        }
    }
}