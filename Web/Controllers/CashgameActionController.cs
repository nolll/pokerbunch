using System;
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
            var actionsOutput = UseCase.Actions(new ActionsInput(slug, dateStr, playerId));
            var actionsChartResult = UseCase.ActionsChart(new ActionsChartRequest(slug, dateStr, playerId, DateTime.UtcNow));
            var model = new ActionPageModel(contextResult, actionsOutput, actionsChartResult);
            return View("~/Views/Pages/CashgameAction/Action.cshtml", model);
        }
    }
}