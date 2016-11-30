using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.LocationModels.Add;
using Web.Routes;
using Web.Urls.SiteUrls;

namespace Web.Controllers
{
    public class AddLocationController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Location.Add)]
        public ActionResult Add(string slug)
        {
            return ShowForm(slug);
        }

        [HttpPost]
        [Authorize]
        [Route(WebRoutes.Location.Add)]
        public ActionResult Add_Post(string slug, AddLocationPostModel postModel)
        {
            try
            {
                var request = new AddLocation.Request(slug, postModel.Name);
                var result = UseCase.AddLocation.Execute(request);
                return Redirect(new AddLocationConfirmationUrl(result.Slug).Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }

            return ShowForm(slug, postModel);
        }

        [Route(WebRoutes.Location.AddConfirmation)]
        public ActionResult Created(string slug)
        {
            var contextResult = GetBunchContext(slug);
            var model = new AddLocationConfirmationPageModel(contextResult);
            return View("~/Views/Pages/AddLocation/AddConfirmation.cshtml", model);
        }

        private ActionResult ShowForm(string slug, AddLocationPostModel postModel = null)
        {
            var contextResult = GetBunchContext(slug);
            var model = new AddLocationPageModel(contextResult, postModel);
            return View("~/Views/Pages/AddLocation/Add.cshtml", model);
        }
    }
}