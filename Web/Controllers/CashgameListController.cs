using System.Web.Mvc;
using Core.UseCases.CashgameContext;
using Core.UseCases.CashgameList;
using Web.Controllers.Base;
using Web.Models.CashgameModels.List;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class CashgameListController : PokerBunchController
    {
        [AuthorizePlayer]
        [Route("{slug}/cashgame/list/{year?}")]
        public ActionResult List(string slug, int? year = null, string orderBy = null)
        {
            var contextResult = UseCase.CashgameContext(new CashgameContextRequest(slug, year, CashgamePage.List));
            var listResult = UseCase.CashgameList(new CashgameListRequest(slug, orderBy, year));

            var model = new CashgameListPageModel(contextResult, listResult);
            return View("~/Views/Pages/CashgameList/List.cshtml", model);
        }
    }
}