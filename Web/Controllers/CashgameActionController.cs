using System.Web.Mvc;
using Core.UseCases.Actions;
using Core.UseCases.BunchContext;
using Web.Controllers.Base;
using Web.ModelFactories.CashgameModelFactories.Action;
using Web.Models.CashgameModels.Action;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class CashgameActionController : PokerBunchController
    {
        private readonly IActionChartJsonBuilder _actionChartJsonBuilder;

        public CashgameActionController(IActionChartJsonBuilder actionChartJsonBuilder)
        {
            _actionChartJsonBuilder = actionChartJsonBuilder;
        }

        [AuthorizePlayer]
        [Route("{slug}/cashgame/action/{dateStr}/{playerId:int}")]
        public ActionResult Action(string slug, string dateStr, int playerId)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var actionsResult = UseCase.Actions(new ActionsRequest(slug, dateStr, playerId));
            var model = new ActionPageModel(contextResult, actionsResult);
            return View("~/Views/Pages/CashgameAction/Action.cshtml", model);
        }

        [AuthorizePlayer]
        [Route("{slug}/cashgame/actionchartjson/{dateStr}/{playerId:int}")]
        public JsonResult ActionChartJson(string slug, string dateStr, int playerId)
        {
            var model = _actionChartJsonBuilder.Build(slug, dateStr, playerId);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}