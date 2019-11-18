using System.Collections.Generic;
using System.Linq;
using Core.Settings;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.EventModels.List
{
	public class EventListPageModel : BunchPageModel
    {
        public IList<EventListItemModel> EventModels { get; }
	    public string AddUrl { get; }

	    public EventListPageModel(AppSettings appSettings, BunchContext.Result contextResult, EventList.Result eventListResult)
            : base(appSettings, contextResult)
	    {
            EventModels = eventListResult.Events.Select(o => new EventListItemModel(o)).ToList();
	        AddUrl = new AddEventUrl(contextResult.Slug).Relative;
	    }

	    public override string BrowserTitle => "Events";

	    public override View GetView()
	    {
	        return new View("~/Views/Pages/EventList/EventList.cshtml");
	    }
    }
}