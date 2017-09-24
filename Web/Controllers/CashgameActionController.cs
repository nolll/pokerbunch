using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Routes;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Action;

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
            var actionsChartResult = UseCase.ActionsChart.Execute(new ActionsChart.Request(cashgameId, playerId));
            var model = new ActionPageModel(contextResult, actionsResult, actionsChartResult);
            return View(model);
        }
    }
}