using System.Web.Mvc;
using Core.Exceptions;
using Core.Urls;
using Core.UseCases.BunchContext;
using Core.UseCases.CashgameDetailsChart;
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
                var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
                var runningCashgameResult = UseCase.RunningCashgame(new RunningCashgameRequest(slug));
                var cashgameDetailsChartResult = UseCase.CashgameDetailsChart(new CashgameDetailsChartRequest(slug));
                var model = new RunningCashgamePageModel(contextResult, runningCashgameResult, cashgameDetailsChartResult);
                return View("~/Views/Pages/RunningCashgame/RunningPage.cshtml", model);
            }
            catch (CashgameNotRunningException)
            {
                return Redirect(new CashgameIndexUrl(slug).Relative);
            }
        }
    }
}