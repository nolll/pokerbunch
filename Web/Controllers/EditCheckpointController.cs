using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Checkpoints;
using Web.Urls;

namespace Web.Controllers
{
    public class EditCheckpointController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.CashgameCheckpointEdit)]
        public ActionResult EditCheckpoint(int id)
        {
            return ShowForm(id);
        }

        [HttpPost]
        [Authorize]
        [Route(WebRoutes.CashgameCheckpointEdit)]
        public ActionResult EditCheckpoint_Post(int id, EditCheckpointPostModel postModel)
        {
            try
            {
                var request = new EditCheckpoint.Request(CurrentUserName, id, postModel.Timestamp, postModel.Stack, postModel.Amount);
                var result = UseCase.EditCheckpoint.Execute(request);
                return Redirect(new CashgameActionUrl(result.CashgameId, result.PlayerId).Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }

            return ShowForm(id, postModel);
        }

        private ActionResult ShowForm(int id, EditCheckpointPostModel postModel = null)
        {
            var editCheckpointFormResult = UseCase.EditCheckpointForm.Execute(new EditCheckpointForm.Request(CurrentUserName, id));
            var contextResult = GetBunchContext(editCheckpointFormResult.Slug);
            var model = new EditCheckpointPageModel(contextResult, editCheckpointFormResult, postModel);
            return View("~/Views/Pages/EditCheckpoint/Edit.cshtml", model);
        }
    }
}