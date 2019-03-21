using System.Collections.Generic;
using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Checkpoints;

namespace Web.Controllers
{
    public class EditActionController : BaseController
    {
        [Authorize]
        [Route(EditActionUrl.Route)]
        public ActionResult EditAction(string cashgameId, string actionId)
        {
            return ShowForm(cashgameId, actionId);
        }

        [HttpPost]
        [Authorize]
        [Route(EditActionUrl.Route)]
        public ActionResult EditAction_Post(string cashgameId, string actionId, EditActionPostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new EditAction.Request(cashgameId, actionId, postModel.Timestamp, postModel.Stack, postModel.Amount);
                var result = UseCase.EditAction.Execute(request);
                return Redirect(new CashgameActionUrl(result.CashgameId, result.PlayerId).Relative);
            }
            catch (ValidationException ex)
            {
                errors.AddRange(ex.Messages);
            }

            return ShowForm(cashgameId, actionId, postModel, errors);
        }

        private ActionResult ShowForm(string cashgameId, string actionId, EditActionPostModel postModel = null, IEnumerable<string> errors = null)
        {
            var editActionForm = UseCase.EditActionForm.Execute(new EditActionForm.Request(cashgameId, actionId));
            var contextResult = GetBunchContext(editActionForm.Slug);
            var model = new EditActionPageModel(contextResult, editActionForm, postModel, errors);
            return View(model);
        }
    }
}