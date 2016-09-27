using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Common.Routes;
using Web.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Edit;

namespace Web.Controllers
{
    public class EditCashgameController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Cashgame.Edit)]
        public ActionResult Edit(int id)
        {
            return ShowForm(id);
        }

        [HttpPost]
        [Authorize]
        [Route(WebRoutes.Cashgame.Edit)]
        public ActionResult Post(int id, EditCashgamePostModel postModel)
        {
            try
            {
                var request = new EditCashgame.Request(Identity.UserName, id, postModel.LocationId, postModel.EventId);
                var result = UseCase.EditCashgame.Execute(request);
                return Redirect(new CashgameDetailsUrl(result.CashgameId).Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }

            return ShowForm(id, postModel);
        }

        private ActionResult ShowForm(int id, EditCashgamePostModel postModel = null)
        {
            var editCashgameFormResult = UseCase.EditCashgameForm.Execute(new EditCashgameForm.Request(Identity.UserName, id));
            var contextResult = GetBunchContext(editCashgameFormResult.Slug);
            var model = new EditCashgamePageModel(contextResult, editCashgameFormResult, postModel);
            return View("~/Views/Pages/EditCashgame/Edit.cshtml", model);
        }
    }
}