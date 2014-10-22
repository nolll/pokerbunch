using System.Web.Mvc;
using Core.UseCases.BunchContext;
using Core.UseCases.EventDetails;
using Web.Controllers.Base;
using Web.Models.EventModels.Details;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class EventDetailsController : PokerBunchController
    {
        [AuthorizePlayer]
        [Route("{slug}/event/details/{id}")]
        public ActionResult List(string slug, int id)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var eventDetailsOutput = UseCase.EventDetails(new EventDetailsInput(id));
            var model = new EventDetailsPageModel(contextResult, eventDetailsOutput);
            return View("~/Views/Pages/EventDetails/EventDetails.cshtml", model);
        }
    }
}