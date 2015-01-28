using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases.BunchContext;
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
            RequireManager(slug);
            return ShowForm(slug, dateStr);
        }

        [HttpPost]
        [Authorize]
        [Route("{slug}/cashgame/edit/{dateStr}")]
        public ActionResult Post(string slug, string dateStr, EditCashgamePostModel postModel)
        {
            RequireManager(slug);
            try
            {
                var request = new EditCashgameRequest(slug, dateStr, postModel.Location);
                var result = UseCase.EditCashgame(request);
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
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var editCashgameFormResult = UseCase.EditCashgameForm(new EditCashgameFormRequest(slug, dateStr));
            var model = new EditCashgamePageModel(contextResult, editCashgameFormResult, postModel);
            return View("~/Views/Pages/EditCashgame/Edit.cshtml", model);
        }
    }
}