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
        public ActionResult DeleteCheckpoint(int id)
        {
            var request = new DeleteCheckpoint.Request(CurrentUserName, id);
            var result = UseCase.DeleteCheckpoint.Execute(request);
            return Redirect(result.ReturnUrl.Relative);
        }
    }
}