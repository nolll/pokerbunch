using System.Web.Mvc;
using Core.UseCases.CashgameContext;
using Core.UseCases.CashgameTopList;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Toplist;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class TopListController : PokerBunchController
    {
        [AuthorizePlayer]
        [Route("{slug}/cashgame/toplist/{year?}")]
        public ActionResult Toplist(string slug, string orderBy = null, int? year = null)
        {
            var contextResult = UseCase.CashgameContext(new CashgameContextRequest(slug, year, CashgamePage.Toplist));
            var topListResult = UseCase.TopList(new TopListRequest(slug, orderBy, year));
            var model = new CashgameToplistPageModel(contextResult, topListResult);
            return View("~/Views/Pages/Toplist/ToplistPage.cshtml", model);
        }
    }
}