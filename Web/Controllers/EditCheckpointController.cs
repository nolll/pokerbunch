using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases.EditCheckpoint;
using Core.UseCases.EditCheckpointForm;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Checkpoints;

namespace Web.Controllers
{
    public class EditCheckpointController : BaseController
    {
        [Authorize]
        [Route("{slug}/cashgame/editcheckpoint/{dateStr}/{playerId:int}/{checkpointId:int}")]
        public ActionResult EditCheckpoint(string slug, string dateStr, int playerId, int checkpointId)
        {
            var context = GetBunchContext(slug);
            RequireManager(context);
            return ShowForm(slug, dateStr, playerId, checkpointId);
        }

        [HttpPost]
        [Authorize]
        [Route("{slug}/cashgame/editcheckpoint/{dateStr}/{playerId:int}/{checkpointId:int}")]
        public ActionResult EditCheckpoint_Post(string slug, string dateStr, int playerId, int checkpointId, EditCheckpointPostModel postModel)
        {
            var context = GetBunchContext(slug);
            RequireManager(context);
            try
            {
                var request = new EditCheckpointRequest(slug, dateStr, playerId, checkpointId, postModel.Timestamp, postModel.Stack, postModel.Amount);
                var result = UseCase.EditCheckpoint.Execute(request);
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
            var contextResult = GetBunchContext(slug);
            var editCheckpointFormResult = UseCase.EditCheckpointForm.Execute(new EditCheckpointFormRequest(slug, dateStr, playerId, checkpointId));
            var model = new EditCheckpointPageModel(contextResult, editCheckpointFormResult, postModel);
            return View("~/Views/Pages/EditCheckpoint/Edit.cshtml", model);
        }
    }
}