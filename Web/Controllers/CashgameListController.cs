using System;
using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.List;

namespace Web.Controllers
{
    public class CashgameListController : BaseController
    {
        [Authorize]
        [Route("{slug}/cashgame/list/{year?}")]
        public ActionResult List(string slug, int? year = null, string orderBy = null)
        {
            var contextResult = GetCashgameContext(slug, DateTime.UtcNow, CashgameContext.CashgamePage.List, year);
            RequirePlayer(contextResult.BunchContext);
            var listResult = UseCase.CashgameList.Execute(new CashgameList.Request(slug, orderBy, year));

            var model = new CashgameListPageModel(contextResult, listResult);
            return View("~/Views/Pages/CashgameList/List.cshtml", model);
        }
    }
}