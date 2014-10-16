using System.Web.Mvc;
using Core.UseCases.Actions;
using Core.UseCases.ActionsChart;
using Core.UseCases.BunchContext;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Action;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class CashgameActionController : PokerBunchController
    {
        [AuthorizePlayer]
        [Route("{slug}/cashgame/action/{dateStr}/{playerId:int}")]
        public ActionResult Action(string slug, string dateStr, int playerId)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var actionsResult = UseCase.Actions(new ActionsInput(slug, dateStr, playerId));
            var model = new ActionPageModel(contextResult, actionsResult);
            return View("~/Views/Pages/CashgameAction/Action.cshtml", model);
        }

        [AuthorizePlayer]
        [Route("{slug}/cashgame/actionchartjson/{dateStr}/{playerId:int}")]
        public JsonResult ActionChartJson(string slug, string dateStr, int playerId)
        {
            var actionsChartResult = UseCase.ActionsChart(new ActionsChartRequest(slug, dateStr, playerId));
            var model = new ActionChartModel(actionsChartResult);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}