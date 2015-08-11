using System.Web.Mvc;
using Core.UseCases;
using Core.UseCases.Matrix;
using Web.Controllers.Base;
using Web.Models.EventModels.Details;

namespace Web.Controllers
{
    public class EventDetailsController : BaseController
    {
        [Authorize]
        [Route("{slug}/event/details/{id}")]
        public ActionResult List(string slug, int id)
        {
            var contextResult = GetBunchContext(slug);
            RequirePlayer(contextResult);
            var eventDetailsOutput = UseCase.EventDetails.Execute(new EventDetails.Request(id));
            var matrixResult = UseCase.Matrix.Execute(new MatrixInteractor.EventMatrixRequest(slug, id));
            var model = new EventDetailsPageModel(contextResult, eventDetailsOutput, matrixResult);
            return View("~/Views/Pages/EventDetails/EventDetails.cshtml", model);
        }
    }
}