using System.Collections.Generic;
using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.HomegameModels.Edit;
using Web.Routes;
using Web.Urls.SiteUrls;

namespace Web.Controllers
{
    public class EditBunchController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Bunch.Edit)]
        public ActionResult Edit(string slug)
        {
            return ShowForm(slug);
        }

        [HttpPost]
        [Authorize]
        [Route(WebRoutes.Bunch.Edit)]
        public ActionResult Edit_Post(string slug, EditBunchPostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new EditBunch.Request(slug, postModel.Description, postModel.CurrencySymbol, postModel.CurrencyLayout, postModel.TimeZone, postModel.HouseRules, postModel.DefaultBuyin);
                var result = UseCase.EditBunch.Execute(request);
                return Redirect(new BunchDetailsUrl(result.BunchId).Relative);
            }
            catch (ValidationException ex)
            {
                errors.AddRange(ex.Messages);
            }
            
            return ShowForm(slug, postModel, errors);
        }

        private ActionResult ShowForm(string slug, EditBunchPostModel postModel = null, IEnumerable<string> errors = null)
        {
            var contextResult = GetBunchContext(slug);
            var editBunchFormRequest = new EditBunchForm.Request(slug);
            var editBunchFormResult = UseCase.EditBunchForm.Execute(editBunchFormRequest);
            var model = new EditBunchPageModel(contextResult, editBunchFormResult, postModel, errors);
            return View(model);
        }
    }
}