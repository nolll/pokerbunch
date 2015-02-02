using System.Web.Mvc;
using Core.UseCases.BunchContext;
using Core.UseCases.EventDetails;
using Web.Controllers.Base;
using Web.Models.EventModels.Details;

namespace Web.Controllers
{
    public class EventDetailsController : PokerBunchController
    {
        [Authorize]
        [Route("{slug}/event/details/{id}")]
        public ActionResult List(string slug, int id)
        {
            RequirePlayer(slug);
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var eventDetailsOutput = UseCase.EventDetails.Execute(new EventDetailsInput(id));
            var model = new EventDetailsPageModel(contextResult, eventDetailsOutput);
            return View("~/Views/Pages/EventDetails/EventDetails.cshtml", model);
        }
    }
}