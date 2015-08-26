using System;
using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Action;
using Web.Urls;

namespace Web.Controllers
{
    public class CashgameActionController : BaseController
    {
        [Authorize]
        [Route(Routes.CashgameAction)]
        public ActionResult Action(int cashgameId, int playerId)
        {
            var actionsResult = UseCase.Actions.Execute(new Actions.Request(CurrentUserName, cashgameId, playerId));
            var contextResult = GetBunchContext(actionsResult.Slug);
            var actionsChartResult = UseCase.ActionsChart.Execute(new ActionsChart.Request(CurrentUserName, cashgameId, playerId, DateTime.UtcNow));
            var model = new ActionPageModel(contextResult, actionsResult, actionsChartResult);
            return View("~/Views/Pages/CashgameAction/Action.cshtml", model);
        }
    }
}