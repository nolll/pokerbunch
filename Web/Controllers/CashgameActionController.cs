using System;
using System.Web.Mvc;
using Core.UseCases.Actions;
using Core.UseCases.ActionsChart;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Action;

namespace Web.Controllers
{
    public class CashgameActionController : PokerBunchController
    {
        [Authorize]
        [Route("{slug}/cashgame/action/{dateStr}/{playerId:int}")]
        public ActionResult Action(string slug, string dateStr, int playerId)
        {
            RequirePlayer(slug);
            var contextResult = GetBunchContext(slug);
            var actionsOutput = UseCase.Actions.Execute(new ActionsInput(slug, dateStr, playerId));
            var actionsChartResult = UseCase.ActionsChart.Execute(new ActionsChartRequest(slug, dateStr, playerId, DateTime.UtcNow));
            var model = new ActionPageModel(contextResult, actionsOutput, actionsChartResult);
            return View("~/Views/Pages/CashgameAction/Action.cshtml", model);
        }
    }
}