using System;
using System.Web.Mvc;
using Core.UseCases.CashgameContext;
using Core.UseCases.CashgameList;
using Web.Controllers.Base;
using Web.Models.CashgameModels.List;

namespace Web.Controllers
{
    public class CashgameListController : PokerBunchController
    {
        [Authorize]
        [Route("{slug}/cashgame/list/{year?}")]
        public ActionResult List(string slug, int? year = null, string orderBy = null)
        {
            RequirePlayer(slug);
            var contextResult = UseCase.CashgameContext.Execute(new CashgameContextRequest(slug, DateTime.UtcNow, CashgamePage.List, year));
            var listResult = UseCase.CashgameList.Execute(new CashgameListRequest(slug, orderBy, year));

            var model = new CashgameListPageModel(contextResult, listResult);
            return View("~/Views/Pages/CashgameList/List.cshtml", model);
        }
    }
}