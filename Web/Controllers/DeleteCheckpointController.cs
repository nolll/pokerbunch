using System.Web.Mvc;
using Core.UseCases.DeleteCheckpoint;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class DeleteCheckpointController : PokerBunchController
    {
        [Authorize]
        [Route("{slug}/cashgame/deletecheckpoint/{dateStr}/{playerId:int}/{checkpointId:int}")]
        public ActionResult DeleteCheckpoint(string slug, string dateStr, int playerId, int checkpointId)
        {
            RequireManager(slug);
            var request = new DeleteCheckpointRequest(slug, dateStr, checkpointId);
            var result = UseCase.DeleteCheckpoint.Execute(request);
            return Redirect(result.ReturnUrl.Relative);
        }
    }
}