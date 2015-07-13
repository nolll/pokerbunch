using System.Web.Mvc;
using Core.Exceptions;
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
            var context = GetBunchContext(slug);
            RequireManager(context);
            return ShowForm(slug);
        }

        [HttpPost]
        [Authorize]
        [Route("{slug}/homegame/edit")]
        public ActionResult Edit_Post(string slug, EditBunchPostModel postModel)
        {
            var context = GetBunchContext(slug);
            RequireManager(context);
            try
            {
                var request = new EditBunchRequest(slug, postModel.Description, postModel.CurrencySymbol, postModel.CurrencyLayout, postModel.TimeZone, postModel.HouseRules, postModel.DefaultBuyin);
                var result = UseCase.EditBunch.Execute(request);
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
            var contextResult = GetBunchContext(slug);
            var editBunchFormRequest = new EditBunchFormRequest(slug);
            var editBunchFormResult = UseCase.EditBunchForm.Execute(editBunchFormRequest);
            var model = new EditBunchPageModel(contextResult, editBunchFormResult, postModel);
            return View("~/Views/Pages/EditBunch/Edit.cshtml", model);
        }
    }
}