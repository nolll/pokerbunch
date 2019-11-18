using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.EventModels.Details;

namespace Web.Controllers
{
    public class EventDetailsController : BunchController
    {
        private readonly EventDetails _eventDetails;
        private readonly EventMatrix _eventMatrix;

        public EventDetailsController(AppSettings appSettings, CoreContext coreContext, BunchContext bunchContext, EventDetails eventDetails, EventMatrix eventMatrix) 
            : base(appSettings, coreContext, bunchContext)
        {
            _eventDetails = eventDetails;
            _eventMatrix = eventMatrix;
        }

        [Authorize]
        [Route(EventDetailsUrl.Route)]
        public ActionResult List(string eventId)
        {
            var eventDetails = _eventDetails.Execute(new EventDetails.Request(eventId));
            var contextResult = GetBunchContext(eventDetails.Slug);
            var matrixResult = _eventMatrix.Execute(new EventMatrix.Request(eventId));
            var model = new EventDetailsPageModel(AppSettings, contextResult, eventDetails, matrixResult);
            return View(model);
        }
    }
}