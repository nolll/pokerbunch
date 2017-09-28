using System.Collections.Generic;
using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.HomegameModels.Edit;

namespace Web.Controllers
{
    public class EditBunchController : BaseController
    {
        [Authorize]
        [Route(EditBunchUrl.Route)]
        public ActionResult Edit(string bunchId)
        {
            return ShowForm(bunchId);
        }

        [HttpPost]
        [Authorize]
        [Route(EditBunchUrl.Route)]
        public ActionResult Edit_Post(string bunchId, EditBunchPostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new EditBunch.Request(bunchId, postModel.Description, postModel.CurrencySymbol, postModel.CurrencyLayout, postModel.TimeZone, postModel.HouseRules, postModel.DefaultBuyin);
                var result = UseCase.EditBunch.Execute(request);
                return Redirect(new BunchDetailsUrl(result.BunchId).Relative);
            }
            catch (ValidationException ex)
            {
                errors.AddRange(ex.Messages);
            }
            
            return ShowForm(bunchId, postModel, errors);
        }

        private ActionResult ShowForm(string bunchId, EditBunchPostModel postModel = null, IEnumerable<string> errors = null)
        {
            var contextResult = GetBunchContext(bunchId);
            var editBunchFormRequest = new EditBunchForm.Request(bunchId);
            var editBunchFormResult = UseCase.EditBunchForm.Execute(editBunchFormRequest);
            var model = new EditBunchPageModel(contextResult, editBunchFormResult, postModel, errors);
            return View(model);
        }
    }
}