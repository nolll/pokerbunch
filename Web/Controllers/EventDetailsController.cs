using System.Web.Mvc;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.EventModels.Details;

namespace Web.Controllers
{
    public class EventDetailsController : BaseController
    {
        [Authorize]
        [Route(Routes.EventDetails)]
        public ActionResult List(string slug, int eventId)
        {
            var contextResult = GetBunchContext(slug);
            var eventDetailsOutput = UseCase.EventDetails.Execute(new EventDetails.Request(CurrentUserName, eventId));
            var matrixResult = UseCase.Matrix.Execute(new Matrix.EventMatrixRequest(CurrentUserName, slug, eventId));
            var model = new EventDetailsPageModel(contextResult, eventDetailsOutput, matrixResult);
            return View("~/Views/Pages/EventDetails/EventDetails.cshtml", model);
        }
    }
}