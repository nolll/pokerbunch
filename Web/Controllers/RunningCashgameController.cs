using System;
using System.Web.Mvc;
using Core.Exceptions;
using Core.Urls;
using Core.UseCases.RunningCashgame;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Running;

namespace Web.Controllers
{
    public class RunningCashgameController : BaseController
    {
        [Authorize]
        [Route("{slug}/cashgame/running")]
        public ActionResult Running(string slug)
        {
            var contextResult = GetBunchContext(slug);
            RequirePlayer(contextResult);
            try
            {
                var runningCashgameResult = UseCase.RunningCashgame.Execute(new RunningCashgameRequest(slug, CurrentUserName, DateTime.UtcNow));
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
            var bunchContext = GetBunchContext(slug);
            RequirePlayer(bunchContext);
            var runningCashgameResult = UseCase.RunningCashgame.Execute(new RunningCashgameRequest(slug, CurrentUserName, DateTime.UtcNow));
            var model = new RunningCashgameJsonModel(runningCashgameResult);
            return JsonView(model);
        }

        [Authorize]
        [Route("{slug}/cashgame/runningplayersjson")]
        public ActionResult RunningPlayersJson(string slug)
        {
            var bunchContext = GetBunchContext(slug);
            RequirePlayer(bunchContext);
            var runningCashgameResult = UseCase.RunningCashgame.Execute(new RunningCashgameRequest(slug, CurrentUserName, DateTime.UtcNow));
            var model = new RunningCashgameRefreshJsonModel(runningCashgameResult);
            return JsonView(model);
        }
    }
}