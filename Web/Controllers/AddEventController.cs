using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.EventModels.Add;
using Web.Routes;
using Web.Urls.SiteUrls;

namespace Web.Controllers
{
    public class AddEventController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Event.Add)]
        public ActionResult Add(string slug)
        {
            return ShowForm(slug);
        }

        [HttpPost]
        [Authorize]
        [Route(WebRoutes.Event.Add)]
        public ActionResult Add_Post(string slug, AddEventPostModel postModel)
        {
            try
            {
                var request = new AddEvent.Request(Identity.UserName, slug, postModel.Name);
                var result = UseCase.AddEvent.Execute(request);
                return Redirect(new AddEventConfirmationUrl(result.Slug).Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }

            return ShowForm(slug, postModel);
        }

        [Route(WebRoutes.Event.AddConfirmation)]
        public ActionResult Created(string slug)
        {
            var contextResult = GetBunchContext(slug);
            var model = new AddEventConfirmationPageModel(contextResult);
            return View("~/Views/Pages/AddEvent/AddConfirmation.cshtml", model);
        }

        private ActionResult ShowForm(string slug, AddEventPostModel postModel = null)
        {
            var contextResult = GetBunchContext(slug);
            var model = new AddEventPageModel(contextResult, postModel);
            return View("~/Views/Pages/AddEvent/Add.cshtml", model);
        }
    }
}