using System;
using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.List;
using Web.Urls;

namespace Web.Controllers
{
    public class CashgameListController : BaseController
    {
        [Authorize]
        [Route(Routes.CashgameList)]
        [Route(Routes.CashgameListWithYear)]
        public ActionResult List(string slug, int? year = null, string orderBy = null)
        {
            var contextResult = GetCashgameContext(slug, DateTime.UtcNow, CashgameContext.CashgamePage.List, year);
            var listResult = UseCase.CashgameList.Execute(new CashgameList.Request(CurrentUserName, slug, ParseListSortOrder(orderBy), year));
            var model = new CashgameListPageModel(contextResult, listResult);
            return View("~/Views/Pages/CashgameList/List.cshtml", model);
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