using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.EventModels.Details;

namespace Web.Controllers
{
    public class EventDetailsController : BaseController
    {
        [Authorize]
        [Route(EventDetailsUrl.Route)]
        public ActionResult List(string eventId)
        {
            var eventDetails = UseCase.EventDetails.Execute(new EventDetails.Request(eventId));
            var contextResult = GetBunchContext(eventDetails.Slug);
            var matrixResult = UseCase.EventMatrix.Execute(new EventMatrix.Request(eventId));
            var model = new EventDetailsPageModel(contextResult, eventDetails, matrixResult);
            return View(model);
        }
    }
}