using System;
using System.Web.Mvc;
using Core.UseCases.CashgameContext;
using Core.UseCases.CashgameTopList;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Toplist;

namespace Web.Controllers
{
    public class TopListController : BaseController
    {
        [Authorize]
        [Route("{slug}/cashgame/toplist/{year?}")]
        public ActionResult Toplist(string slug, string orderBy = null, int? year = null)
        {
            var contextResult = GetCashgameContext(slug, DateTime.UtcNow, CashgamePage.Toplist, year);
            RequirePlayer(contextResult.BunchContext);
            var topListResult = UseCase.TopList.Execute(new TopListRequest(slug, orderBy, year));
            var model = new CashgameToplistPageModel(contextResult, topListResult);
            return View("~/Views/Pages/Toplist/ToplistPage.cshtml", model);
        }
    }
}