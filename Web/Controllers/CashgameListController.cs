using System;
using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.CashgameModels.List;

namespace Web.Controllers
{
    public class CashgameListController : BaseController
    {
        [Authorize]
        [Route(ListUrl.Route)]
        [Route(ListUrl.RouteWithYear)]
        public ActionResult List(string bunchId, int? year = null, string orderBy = null)
        {
            var contextResult = GetCashgameContext(bunchId, DateTime.UtcNow, CashgameContext.CashgamePage.List, year);
            var listResult = UseCase.CashgameList.Execute(new CashgameList.Request(bunchId, ParseListSortOrder(orderBy), year));
            var model = new CashgameListPageModel(contextResult, listResult);
            return View(model);
        }

        private static CashgameList.SortOrder ParseListSortOrder(string s)
        {
            if (s == null)
                return CashgameList.SortOrder.Date;
            CashgameList.SortOrder sortOrder;
            return Enum.TryParse(s, true, out sortOrder) ? sortOrder : CashgameList.SortOrder.Date;
        }
    }
}