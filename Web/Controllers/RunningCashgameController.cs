using System;
using System.Web.Mvc;
using Core.Exceptions;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Running;

namespace Web.Controllers
{
    public class RunningCashgameController : BaseController
    {
        [Authorize]
        [Route(Routes.RunningCashgame)]
        public ActionResult Running(string slug)
        {
            var contextResult = GetBunchContextBySlug(slug);
            try
            {
                var runningCashgameResult = UseCase.RunningCashgame.Execute(new RunningCashgame.Request(CurrentUserName, slug, DateTime.UtcNow));
                var model = new RunningCashgamePageModel(contextResult, runningCashgameResult);
                return View("~/Views/Pages/RunningCashgame/RunningPage.cshtml", model);
            }
            catch (CashgameNotRunningException)
            {
                return Redirect(new CashgameIndexUrl(slug).Relative);
            }
        }

        [Authorize]
        [Route(Routes.RunningCashgameGameJson)]
        public ActionResult RunningGameJson(string slug)
        {
            var runningCashgameResult = UseCase.RunningCashgame.Execute(new RunningCashgame.Request(CurrentUserName, slug, DateTime.UtcNow));
            var model = new RunningCashgameJsonModel(runningCashgameResult);
            return JsonView(model);
        }

        [Authorize]
        [Route(Routes.RunningCashgamePlayersJson)]
        public ActionResult RunningPlayersJson(string slug)
        {
            var runningCashgameResult = UseCase.RunningCashgame.Execute(new RunningCashgame.Request(CurrentUserName, slug, DateTime.UtcNow));
            var model = new RunningCashgameRefreshJsonModel(runningCashgameResult);
            return JsonView(model);
        }
    }
}