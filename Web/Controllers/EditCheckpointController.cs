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
        public ActionResult EditCheckpoint(string cashgameId, string id)
        {
            return ShowForm(cashgameId, id);
        }

        [HttpPost]
        [Authorize]
        [Route(EditCheckpointUrl.Route)]
        public ActionResult EditCheckpoint_Post(string cashgameId, string id, EditCheckpointPostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new EditCheckpoint.Request(Identity.UserName, id, postModel.Timestamp, postModel.Stack, postModel.Amount);
                var result = UseCase.EditCheckpoint.Execute(request);
                return Redirect(new CashgameActionUrl(result.CashgameId, result.PlayerId).Relative);
            }
            catch (ValidationException ex)
            {
                errors.AddRange(ex.Messages);
            }

            return ShowForm(cashgameId, id, postModel, errors);
        }

        private ActionResult ShowForm(string cashgameId, string id, EditCheckpointPostModel postModel = null, IEnumerable<string> errors = null)
        {
            var editCheckpointFormResult = UseCase.EditCheckpointForm.Execute(new EditCheckpointForm.Request(cashgameId, id));
            var contextResult = GetBunchContext(editCheckpointFormResult.Slug);
            var model = new EditCheckpointPageModel(contextResult, editCheckpointFormResult, postModel, errors);
            return View(model);
        }
    }
}