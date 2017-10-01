using System.Collections.Generic;
using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Checkpoints;

namespace Web.Controllers
{
    public class EditCheckpointController : BaseController
    {
        [Authorize]
        [Route(EditCheckpointUrl.Route)]
        public ActionResult EditCheckpoint(string cashgameId, string actionId)
        {
            return ShowForm(cashgameId, actionId);
        }

        [HttpPost]
        [Authorize]
        [Route(EditCheckpointUrl.Route)]
        public ActionResult EditCheckpoint_Post(string cashgameId, string actionId, EditCheckpointPostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new EditCheckpoint.Request(cashgameId, actionId, postModel.Timestamp, postModel.Stack, postModel.Amount);
                var result = UseCase.EditCheckpoint.Execute(request);
                return Redirect(new CashgameActionUrl(result.CashgameId, result.PlayerId).Relative);
            }
            catch (ValidationException ex)
            {
                errors.AddRange(ex.Messages);
            }

            return ShowForm(cashgameId, actionId, postModel, errors);
        }

        private ActionResult ShowForm(string cashgameId, string actionId, EditCheckpointPostModel postModel = null, IEnumerable<string> errors = null)
        {
            var editCheckpointFormResult = UseCase.EditCheckpointForm.Execute(new EditCheckpointForm.Request(cashgameId, actionId));
            var contextResult = GetBunchContext(editCheckpointFormResult.Slug);
            var model = new EditCheckpointPageModel(contextResult, editCheckpointFormResult, postModel, errors);
            return View(model);
        }
    }
}