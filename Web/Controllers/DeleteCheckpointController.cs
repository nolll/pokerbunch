using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Urls;

namespace Web.Controllers
{
    public class DeleteCheckpointController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.CashgameCheckpointDelete)]
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