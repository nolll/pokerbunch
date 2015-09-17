using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Running;
using Web.Urls;

namespace Web.Controllers
{
    public class RunningCashgameController : BaseController
    {
        [Authorize]
        [Route(Routes.RunningCashgame)]
        public ActionResult Running(string slug)
        {
            var contextResult = GetBunchContext(slug);
            try
            {
                var runningCashgameResult = UseCase.RunningCashgame.Execute(new RunningCashgame.Request(CurrentUserName, slug));
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
            try
            {
                var runningCashgameResult = UseCase.RunningCashgame.Execute(new RunningCashgame.Request(CurrentUserName, slug));
                var model = new RunningCashgameJsonModel(runningCashgameResult);
                return JsonView(model);
            }
            catch (CashgameNotRunningException e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent, e.Message);
            }
        }

        [Authorize]
        [Route(Routes.RunningCashgamePlayersJson)]
        public ActionResult RunningPlayersJson(string slug)
        {
            var runningCashgameResult = UseCase.RunningCashgame.Execute(new RunningCashgame.Request(CurrentUserName, slug));
            var model = new RunningCashgameRefreshJsonModel(runningCashgameResult);
            return JsonView(model);
        }
    }
}