using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases.BunchContext;
using Core.UseCases.EditBunch;
using Core.UseCases.EditBunchForm;
using Web.Controllers.Base;
using Web.Models.HomegameModels.Edit;

namespace Web.Controllers
{
    public class EditBunchController : PokerBunchController
    {
        [Authorize]
        [Route("{slug}/homegame/edit")]
        public ActionResult Edit(string slug)
        {
            RequireManager(slug);
            return ShowForm(slug);
        }

        [HttpPost]
        [Authorize]
        [Route("{slug}/homegame/edit")]
        public ActionResult Edit_Post(string slug, EditBunchPostModel postModel)
        {
            RequireManager(slug);
            try
            {
                var request = new EditBunchRequest(slug, postModel.Description, postModel.CurrencySymbol, postModel.CurrencyLayout, postModel.TimeZone, postModel.HouseRules, postModel.DefaultBuyin);
                var result = UseCase.EditBunch(request);
                return Redirect(result.ReturnUrl.Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }
            
            return ShowForm(slug, postModel);
        }

        private ActionResult ShowForm(string slug, EditBunchPostModel postModel = null)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var editBunchFormRequest = new EditBunchFormRequest(slug);
            var editBunchFormResult = UseCase.EditBunchForm(editBunchFormRequest);
            var model = new EditBunchPageModel(contextResult, editBunchFormResult, postModel);
            return View("~/Views/Pages/EditBunch/Edit.cshtml", model);
        }
    }
}