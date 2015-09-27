using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.HomegameModels.Add;
using Web.Urls;

namespace Web.Controllers
{
    public class AddBunchController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.BunchAdd)]
        public ActionResult Add()
        {
            return ShowForm();
        }

        [HttpPost]
        [Authorize]
        [Route(WebRoutes.BunchAdd)]
        public ActionResult Add_Post(AddBunchPostModel postModel)
        {
            try
            {
                var request = new AddBunch.Request(CurrentUserName, postModel.DisplayName, postModel.Description, postModel.CurrencySymbol, postModel.CurrencyLayout, postModel.TimeZone);
                UseCase.AddBunch.Execute(request);
                return Redirect(new AddBunchConfirmationUrl().Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }
            catch (BunchExistsException ex)
            {
                AddModelError(ex.Message);
            }

            return ShowForm(postModel);
        }

        [Route(WebRoutes.BunchAddConfirmation)]
        public ActionResult Created()
        {
            var contextResult = GetAppContext();
            var model = new AddBunchConfirmationPageModel(contextResult);
            return View("~/Views/Pages/AddBunch/AddBunchConfirmation.cshtml", model);
        }

        private ActionResult ShowForm(AddBunchPostModel postModel = null)
        {
            var contextResult = GetAppContext();
            var bunchFormResult = UseCase.AddBunchForm.Execute();
            var model = new AddBunchPageModel(contextResult, bunchFormResult, postModel);
            return View("~/Views/Pages/AddBunch/AddBunch.cshtml", model);
        }
    }
}