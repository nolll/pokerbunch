using System;
using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Toplist;
using Web.Urls;

namespace Web.Controllers
{
    public class TopListController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.CashgameToplist)]
        [Route(WebRoutes.CashgameToplistWithYear)]
        public ActionResult Toplist(string slug, string orderBy = null, int? year = null)
        {
            var contextResult = GetCashgameContext(slug, DateTime.UtcNow, CashgameContext.CashgamePage.Toplist, year);
            var topListResult = UseCase.TopList.Execute(new TopList.Request(CurrentUserName, slug, ParseToplistSortOrder(orderBy), year));
            var model = new CashgameToplistPageModel(contextResult, topListResult);
            return View("~/Views/Pages/Toplist/ToplistPage.cshtml", model);
        }

        private static TopList.SortOrder ParseToplistSortOrder(string s)
        {
            if (s == null)
                return TopList.SortOrder.Winnings;
            TopList.SortOrder sortOrder;
            return Enum.TryParse(s, true, out sortOrder) ? sortOrder : TopList.SortOrder.Winnings;
        }
    }
}