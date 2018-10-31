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
        [Route(RunningCashgameGameJsonUrl.Route)]
        public ActionResult RunningGameJson(string bunchId)
        {
            try
            {
                var runningCashgameResult = UseCase.RunningCashgame.Execute(new RunningCashgame.Request(Identity.UserName, bunchId));
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
        public ActionResult RunningPlayersJson(string bunchId)
        {
            var runningCashgameResult = UseCase.RunningCashgame.Execute(new RunningCashgame.Request(Identity.UserName, bunchId));
            var model = new RunningCashgameRefreshJsonModel(runningCashgameResult);
            return JsonView(model);
        }
    }
}