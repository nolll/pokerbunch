using System.Web.Mvc;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class DeleteCheckpointController : BaseController
    {
        [Authorize]
        [Route(Routes.CashgameCheckpointDelete)]
        public ActionResult DeleteCheckpoint(string slug, string dateStr, int playerId, int checkpointId)
        {
            var request = new DeleteCheckpoint.Request(CurrentUserName, slug, dateStr, checkpointId);
            var result = UseCase.DeleteCheckpoint.Execute(request);
            return Redirect(result.ReturnUrl.Relative);
        }
    }
}