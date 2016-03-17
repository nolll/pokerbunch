using System;
using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Toplist;

namespace Web.Controllers
{
    public class TopListController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Cashgame.Toplist)]
        [Route(WebRoutes.Cashgame.ToplistWithYear)]
        public ActionResult Toplist(string slug, int? year = null)
        {
            var contextResult = GetCashgameContext(slug, DateTime.UtcNow, CashgameContext.CashgamePage.Toplist, year);
            var topListResult = UseCase.TopList.Execute(new TopList.Request(CurrentUserName, slug, year));
            var model = new CashgameToplistPageModel(contextResult, topListResult);
            return View("~/Views/Pages/Toplist/ToplistPage.cshtml", model);
        }
    }
}