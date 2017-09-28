using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class DeleteCheckpointController : BaseController
    {
        [Authorize]
        [Route(DeleteCheckpointUrl.Route)]
        public ActionResult DeleteCheckpoint(string cashgameId, string actionId)
        {
            var request = new DeleteCheckpoint.Request(cashgameId, actionId);
            var result = UseCase.DeleteCheckpoint.Execute(request);
            return Redirect(GetReturnUrl(result).Relative);
        }

        private static SiteUrl GetReturnUrl(DeleteCheckpoint.Result result)
        {
            if (result.GameIsRunning)
                return new RunningCashgameUrl(result.Slug);
            return new CashgameDetailsUrl(result.CashgameId);
        }
    }
}