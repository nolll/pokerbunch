using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.EventModels.List;

namespace Web.Controllers
{
    public class EventListController : BunchController
    {
        private readonly EventList _eventList;

        public EventListController(AppSettings appSettings, CoreContext coreContext, BunchContext bunchContext, EventList eventList) 
            : base(appSettings, coreContext, bunchContext)
        {
            _eventList = eventList;
        }

        [Authorize]
        [Route(EventListUrl.Route)]
        public ActionResult List(string bunchId)
        {
            var contextResult = GetBunchContext(bunchId);
            var eventListOutput = _eventList.Execute(new EventList.Request(bunchId));
            var model = new EventListPageModel(AppSettings, contextResult, eventListOutput);
            return View(model);
        }
    }
}