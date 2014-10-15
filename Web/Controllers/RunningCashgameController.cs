using System.Web.Mvc;
using Core.Exceptions;
using Core.Urls;
using Core.UseCases.CashgameContext;
using Core.UseCases.RunningCashgame;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Running;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class RunningCashgameController : PokerBunchController
    {
        [AuthorizePlayer]
        [Route("{slug}/cashgame/running")]
        public ActionResult Running(string slug)
        {
            try
            {
                var contextResult = UseCase.CashgameContext(new CashgameContextRequest(slug));
                var runningCashgameResult = UseCase.RunningCashgame(new RunningCashgameRequest(slug));
                var model = new RunningCashgamePageModel(contextResult, runningCashgameResult);
                return View("~/Views/Pages/RunningCashgame/RunningPage.cshtml", model);
            }
            catch (CashgameNotRunningException)
            {
                return Redirect(new CashgameIndexUrl(slug).Relative);
            }
        }
    }
}