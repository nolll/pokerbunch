using System.Web.Mvc;
using Core.Exceptions;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Checkpoints;

namespace Web.Controllers
{
    public class EditCheckpointController : BaseController
    {
        [Authorize]
        [Route(Routes.CashgameCheckpointEdit)]
        public ActionResult EditCheckpoint(string slug, string dateStr, int playerId, int checkpointId)
        {
            var context = GetBunchContext(slug);
            RequireManager(context);
            return ShowForm(slug, dateStr, playerId, checkpointId);
        }

        [HttpPost]
        [Authorize]
        [Route(Routes.CashgameCheckpointEdit)]
        public ActionResult EditCheckpoint_Post(string slug, string dateStr, int playerId, int checkpointId, EditCheckpointPostModel postModel)
        {
            var context = GetBunchContext(slug);
            RequireManager(context);
            try
            {
                var request = new EditCheckpoint.Request(slug, dateStr, playerId, checkpointId, postModel.Timestamp, postModel.Stack, postModel.Amount);
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
            var editCheckpointFormResult = UseCase.EditCheckpointForm.Execute(new EditCheckpointForm.Request(slug, dateStr, playerId, checkpointId));
            var model = new EditCheckpointPageModel(contextResult, editCheckpointFormResult, postModel);
            return View("~/Views/Pages/EditCheckpoint/Edit.cshtml", model);
        }
    }
}