using System.Web.Mvc;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Urls;

namespace Web.Controllers
{
    public class DeleteCheckpointController : BaseController
    {
        [Authorize]
        [Route(Routes.CashgameCheckpointDelete)]
        public ActionResult DeleteCheckpoint(int id)
        {
            var request = new DeleteCheckpoint.Request(CurrentUserName, id);
            var result = UseCase.DeleteCheckpoint.Execute(request);
            return Redirect(GetReturnUrl(result).Relative);
        }

        private static Url GetReturnUrl(DeleteCheckpoint.Result result)
        {
            if (result.GameIsRunning)
                return new RunningCashgameUrl(result.Slug);
            return new CashgameDetailsUrl(result.CashgameId);
        }
    }
}