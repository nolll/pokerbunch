using System.Net;
using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Running;

namespace Web.Controllers
{
    public class RunningCashgameController : BaseController
    {
        [Authorize]
        [Route(RunningCashgameUrl.Route)]
        public ActionResult Running(string slug)
        {
            var contextResult = GetBunchContext(slug);
            try
            {
                var runningCashgameResult = UseCase.RunningCashgame.Execute(new RunningCashgame.Request(Identity.UserName, slug));
                var model = new RunningCashgamePageModel(contextResult, runningCashgameResult);
                return View(model);
            }
            catch (CashgameNotRunningException)
            {
                return Redirect(new CashgameIndexUrl(slug).Relative);
            }
        }

        [Authorize]
        [Route(DashboardUrl.Route)]
        public ActionResult Dashboard(string slug)
        {
            var contextResult = GetBaseContext();
            try
            {
                var runningCashgameResult = UseCase.RunningCashgame.Execute(new RunningCashgame.Request(Identity.UserName, slug));
                var model = new CashgameDashboardPageModel(contextResult, runningCashgameResult);
                return View(model);
            }
            catch (CashgameNotRunningException)
            {
                return Redirect(new CashgameIndexUrl(slug).Relative);
            }
        }

        [Authorize]
        [Route(RunningCashgameGameJsonUrl.Route)]
        public ActionResult RunningGameJson(string slug)
        {
            try
            {
                var runningCashgameResult = UseCase.RunningCashgame.Execute(new RunningCashgame.Request(Identity.UserName, slug));
                var model = new RunningCashgameJsonModel(runningCashgameResult);
                return JsonView(model);
            }
            catch (CashgameNotRunningException e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent, e.Message);
            }
        }

        [Authorize]
        [Route(RunningCashgamePlayersJsonUrl.Route)]
        public ActionResult RunningPlayersJson(string slug)
        {
            var runningCashgameResult = UseCase.RunningCashgame.Execute(new RunningCashgame.Request(Identity.UserName, slug));
            var model = new RunningCashgameRefreshJsonModel(runningCashgameResult);
            return JsonView(model);
        }
    }
}