using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Routes;
using Web.Common.Urls.SiteUrls;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class DeleteCheckpointController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Cashgame.CheckpointDelete)]
        public ActionResult DeleteCheckpoint(int id)
        {
            var request = new DeleteCheckpoint.Request(CurrentUserName, id);
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