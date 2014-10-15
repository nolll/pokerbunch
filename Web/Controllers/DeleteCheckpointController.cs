using System.Web.Mvc;
using Core.UseCases.DeleteCheckpoint;
using Web.Controllers.Base;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class DeleteCheckpointController : PokerBunchController
    {
        [AuthorizeManager]
        [Route("{slug}/cashgame/deletecheckpoint/{dateStr}/{playerId:int}/{checkpointId:int}")]
        public ActionResult DeleteCheckpoint(string slug, string dateStr, int playerId, int checkpointId)
        {
            var request = new DeleteCheckpointRequest(slug, dateStr, checkpointId);
            var result = UseCase.DeleteCheckpoint(request);
            return Redirect(result.ReturnUrl.Relative);
        }
    }
}