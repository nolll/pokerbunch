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
        [Route(TopListWithYearUrl.Route)]
        public ActionResult Toplist(string slug, int? year = null)
        {
            var contextResult = GetCashgameContext(slug, DateTime.UtcNow, CashgameContext.CashgamePage.Toplist, year);
            var topListResult = UseCase.TopList.Execute(new TopList.Request(slug, year));
            var model = new CashgameToplistPageModel(contextResult, topListResult);
            return View(model);
        }
    }
}