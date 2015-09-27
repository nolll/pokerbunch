using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.EventModels.Details;

namespace Web.Controllers
{
    public class EventDetailsController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.EventDetails)]
        public ActionResult List(int id)
        {
            var eventDetails = UseCase.EventDetails.Execute(new EventDetails.Request(CurrentUserName, id));
            var contextResult = GetBunchContext(eventDetails.Slug);
            var matrixResult = UseCase.Matrix.Execute(new Matrix.EventMatrixRequest(CurrentUserName, id));
            var model = new EventDetailsPageModel(contextResult, eventDetails, matrixResult);
            return View("~/Views/Pages/EventDetails/EventDetails.cshtml", model);
        }
    }
}