using System;
using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Toplist;

namespace Web.Controllers
{
    public class TopListController : BaseController
    {
        [Authorize]
        [Route(TopListUrl.Route)]
        [Route(TopListUrl.RouteWithYear)]
        public ActionResult Toplist(string bunchId, int? year = null)
        {
            var contextResult = GetCashgameContext(bunchId, DateTime.UtcNow, CashgameContext.CashgamePage.Toplist, year);
            var topListResult = UseCase.TopList.Execute(new TopList.Request(bunchId, year));
            var model = new CashgameToplistPageModel(contextResult, topListResult);
            return View(model);
        }
    }
}