using System;
using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Action;

namespace Web.Controllers
{
    public class CashgameActionController : BaseController
    {
        [Authorize]
        [Route("{slug}/cashgame/action/{dateStr}/{playerId:int}")]
        public ActionResult Action(string slug, string dateStr, int playerId)
        {
            var contextResult = GetBunchContext(slug);
            RequirePlayer(contextResult);
            var actionsOutput = UseCase.Actions.Execute(new Actions.Request(CurrentUserName, slug, dateStr, playerId));
            var actionsChartResult = UseCase.ActionsChart.Execute(new ActionsChart.Request(slug, dateStr, playerId, DateTime.UtcNow));
            var model = new ActionPageModel(contextResult, actionsOutput, actionsChartResult);
            return View("~/Views/Pages/CashgameAction/Action.cshtml", model);
        }
    }
}