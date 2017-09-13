using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.EventModels.Details;
using Web.Routes;

namespace Web.Controllers
{
    public class EventDetailsController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Event.Details)]
        public ActionResult List(string id)
        {
            var eventDetails = UseCase.EventDetails.Execute(new EventDetails.Request(id));
            var contextResult = GetBunchContext(eventDetails.Slug);
            var matrixResult = UseCase.EventMatrix.Execute(new EventMatrix.Request(id));
            var model = new EventDetailsPageModel(contextResult, eventDetails, matrixResult);
            return View(model);
        }
    }
}