using System;
using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Action;
using Web.Routes;

namespace Web.Controllers
{
    public class CashgameActionController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Cashgame.Action)]
        public ActionResult Action(string cashgameId, string playerId)
        {
            var actionsResult = UseCase.Actions.Execute(new Actions.Request(cashgameId, playerId));
            var contextResult = GetBunchContext(actionsResult.Slug);
            var actionsChartResult = UseCase.ActionsChart.Execute(new ActionsChart.Request(Identity.UserName, cashgameId, playerId, DateTime.UtcNow));
            var model = new ActionPageModel(contextResult, actionsResult, actionsChartResult);
            return View("~/Views/Pages/CashgameAction/Action.cshtml", model);
        }
    }
}