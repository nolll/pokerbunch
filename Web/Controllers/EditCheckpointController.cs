using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases.BunchContext;
using Core.UseCases.EditCheckpoint;
using Core.UseCases.EditCheckpointForm;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Checkpoints;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class EditCheckpointController : PokerBunchController
    {
        [AuthorizeManager]
        [Route("{slug}/cashgame/editcheckpoint/{dateStr}/{playerId:int}/{checkpointId:int}")]
        public ActionResult EditCheckpoint(string slug, string dateStr, int playerId, int checkpointId)
        {
            return ShowForm(slug, dateStr, playerId, checkpointId);
        }

        [HttpPost]
        [AuthorizeManager]
        [Route("{slug}/cashgame/editcheckpoint/{dateStr}/{playerId:int}/{checkpointId:int}")]
        public ActionResult EditCheckpoint_Post(string slug, string dateStr, int playerId, int checkpointId, EditCheckpointPostModel postModel)
        {
            try
            {
                var request = new EditCheckpointRequest(slug, dateStr, playerId, checkpointId, postModel.Timestamp, postModel.Stack, postModel.Amount);
                var result = UseCase.EditCheckpoint(request);
                return Redirect(result.ReturnUrl.Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }

            return ShowForm(slug, dateStr, playerId, checkpointId, postModel);
        }

        private ActionResult ShowForm(string slug, string dateStr, int playerId, int checkpointId, EditCheckpointPostModel postModel = null)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var editCheckpointFormResult = UseCase.EditCheckpointForm(new EditCheckpointFormRequest(slug, dateStr, playerId, checkpointId));
            var model = new EditCheckpointPageModel(contextResult, editCheckpointFormResult, postModel);
            return View("~/Views/Pages/EditCheckpoint/Edit.cshtml", model);
        }
    }
}