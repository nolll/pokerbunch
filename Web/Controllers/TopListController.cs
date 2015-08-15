using System;
using System.Web.Mvc;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Toplist;

namespace Web.Controllers
{
    public class TopListController : BaseController
    {
        [Authorize]
        [Route(Routes.CashgameToplist)]
        [Route(Routes.CashgameToplistWithYear)]
        public ActionResult Toplist(string slug, string orderBy = null, int? year = null)
        {
            var contextResult = GetCashgameContext(slug, DateTime.UtcNow, CashgameContext.CashgamePage.Toplist, year);
            RequirePlayer(contextResult.BunchContext);
            var topListResult = UseCase.TopList.Execute(new TopList.Request(slug, orderBy, year));
            var model = new CashgameToplistPageModel(contextResult, topListResult);
            return View("~/Views/Pages/Toplist/ToplistPage.cshtml", model);
        }
    }
}