using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases.EditCashgame;
using Core.UseCases.EditCashgameForm;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Edit;

namespace Web.Controllers
{
    public class EditCashgameController : PokerBunchController
    {
        [Authorize]
        [Route("{slug}/cashgame/edit/{dateStr}")]
        public ActionResult Edit(string slug, string dateStr)
        {
            var context = GetBunchContext(slug);
            RequireManager(context);
            return ShowForm(slug, dateStr);
        }

        [HttpPost]
        [Authorize]
        [Route("{slug}/cashgame/edit/{dateStr}")]
        public ActionResult Post(string slug, string dateStr, EditCashgamePostModel postModel)
        {
            var context = GetBunchContext(slug);
            RequireManager(context);
            try
            {
                var request = new EditCashgameRequest(slug, dateStr, postModel.Location);
                var result = UseCase.EditCashgame.Execute(request);
                return Redirect(result.ReturnUrl.Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }

            return ShowForm(slug, dateStr, postModel);
        }

        private ActionResult ShowForm(string slug, string dateStr, EditCashgamePostModel postModel = null)
        {
            var contextResult = GetBunchContext(slug);
            var editCashgameFormResult = UseCase.EditCashgameForm.Execute(new EditCashgameFormRequest(slug, dateStr));
            var model = new EditCashgamePageModel(contextResult, editCashgameFormResult, postModel);
            return View("~/Views/Pages/EditCashgame/Edit.cshtml", model);
        }
    }
}