using System.Web.Mvc;
using Core.Exceptions;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Edit;

namespace Web.Controllers
{
    public class EditCashgameController : BaseController
    {
        [Authorize]
        [Route(Routes.CashgameEdit)]
        public ActionResult Edit(string slug, string dateStr)
        {
            return ShowForm(slug, dateStr);
        }

        [HttpPost]
        [Authorize]
        [Route(Routes.CashgameEdit)]
        public ActionResult Post(string slug, string dateStr, EditCashgamePostModel postModel)
        {
            try
            {
                var request = new EditCashgame.Request(CurrentUserName, slug, dateStr, postModel.Location);
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
            var contextResult = GetBunchContextBySlug(slug);
            var editCashgameFormResult = UseCase.EditCashgameForm.Execute(new EditCashgameForm.Request(CurrentUserName, slug, dateStr));
            var model = new EditCashgamePageModel(contextResult, editCashgameFormResult, postModel);
            return View("~/Views/Pages/EditCashgame/Edit.cshtml", model);
        }
    }
}