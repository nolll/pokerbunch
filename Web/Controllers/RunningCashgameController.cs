using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Common.Routes;
using Web.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Running;

namespace Web.Controllers
{
    public class RunningCashgameController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.RunningCashgame)]
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
        [Route(WebRoutes.RunningCashgameGameJson)]
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
        [Route(WebRoutes.RunningCashgamePlayersJson)]
        public ActionResult RunningPlayersJson(string slug)
        {
            var runningCashgameResult = UseCase.RunningCashgame.Execute(new RunningCashgame.Request(CurrentUserName, slug));
            var model = new RunningCashgameRefreshJsonModel(runningCashgameResult);
            return JsonView(model);
        }
    }
}