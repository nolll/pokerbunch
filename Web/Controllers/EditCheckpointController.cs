using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Checkpoints;
using Web.Routes;
using Web.Urls.SiteUrls;

namespace Web.Controllers
{
    public class EditCheckpointController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Cashgame.CheckpointEdit)]
        public ActionResult EditCheckpoint(string cashgameId, string id)
        {
            return ShowForm(cashgameId, id);
        }

        [HttpPost]
        [Authorize]
        [Route(WebRoutes.Cashgame.CheckpointEdit)]
        public ActionResult EditCheckpoint_Post(string cashgameId, string id, EditCheckpointPostModel postModel)
        {
            try
            {
                var request = new EditCheckpoint.Request(Identity.UserName, id, postModel.Timestamp, postModel.Stack, postModel.Amount);
                var result = UseCase.EditCheckpoint.Execute(request);
                return Redirect(new CashgameActionUrl(result.CashgameId, result.PlayerId).Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }

            return ShowForm(cashgameId, id, postModel);
        }

        private ActionResult ShowForm(string cashgameId, string id, EditCheckpointPostModel postModel = null)
        {
            var editCheckpointFormResult = UseCase.EditCheckpointForm.Execute(new EditCheckpointForm.Request(cashgameId, id));
            var contextResult = GetBunchContext(editCheckpointFormResult.Slug);
            var model = new EditCheckpointPageModel(contextResult, editCheckpointFormResult, postModel);
            return View("~/Views/Pages/EditCheckpoint/Edit.cshtml", model);
        }
    }
}