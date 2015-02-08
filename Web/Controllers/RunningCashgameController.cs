using System;
using System.Web.Mvc;
using Core.Exceptions;
using Core.Urls;
using Core.UseCases.BunchContext;
using Core.UseCases.CashgameDetailsChart;
using Core.UseCases.RunningCashgame;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Board;
using Web.Models.CashgameModels.Running;

namespace Web.Controllers
{
    public class RunningCashgameController : PokerBunchController
    {
        [Authorize]
        [Route("{slug}/cashgame/running")]
        public ActionResult Running(string slug)
        {
            RequirePlayer(slug);
            try
            {
                var contextResult = UseCase.BunchContext.Execute(new BunchContextRequest(slug));
                var runningCashgameResult = UseCase.RunningCashgame.Execute(new RunningCashgameRequest(slug, Identity.UserId, DateTime.UtcNow));
                var model = new RunningCashgamePageModel(contextResult, runningCashgameResult);
                return View("~/Views/Pages/RunningCashgame/RunningPage.cshtml", model);
            }
            catch (CashgameNotRunningException)
            {
                return Redirect(new CashgameIndexUrl(slug).Relative);
            }
        }

        [Authorize]
        [Route("{slug}/cashgame/runninggamejson")]
        public ActionResult RunningGameJson(string slug)
        {
            RequirePlayer(slug);
            var runningCashgameResult = UseCase.RunningCashgame.Execute(new RunningCashgameRequest(slug, Identity.UserId, DateTime.UtcNow));
            var model = new RunningCashgameJsonModel(runningCashgameResult);
            return JsonView(model);
        }

        [Authorize]
        [Route("{slug}/cashgame/runningplayersjson")]
        public ActionResult RunningPlayersJson(string slug)
        {
            RequirePlayer(slug);
            var runningCashgameResult = UseCase.RunningCashgame.Execute(new RunningCashgameRequest(slug, Identity.UserId, DateTime.UtcNow));
            var model = new RunningCashgameRefreshJsonModel(runningCashgameResult);
            return JsonView(model);
        }

        [Authorize]
        [Route("{slug}/cashgame/board")]
        public ActionResult Board(string slug)
        {
            RequirePlayer(slug);
            try
            {
                var contextResult = UseCase.BaseContext.Execute();
                var runningCashgameResult = UseCase.RunningCashgame.Execute(new RunningCashgameRequest(slug, Identity.UserId, DateTime.UtcNow));
                var cashgameDetailsChartResult = UseCase.CashgameDetailsChart.Execute(new CashgameDetailsChartRequest(slug, DateTime.UtcNow));
                var model = new CashgameBoardPageModel(contextResult, runningCashgameResult, cashgameDetailsChartResult);
                return View("~/Views/Pages/CashgameBoard/BoardPage.cshtml", model);
            }
            catch (CashgameNotRunningException)
            {
                return Redirect(new CashgameIndexUrl(slug).Relative);
            }
        }
    }
}