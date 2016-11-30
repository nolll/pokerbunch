using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using Web.Models.PageBaseModels;
using Web.Urls.SiteUrls;

namespace Web.Models.EventModels.List
{
	public class EventListPageModel : BunchPageModel
    {
        public IList<EventListItemModel> EventModels { get; private set; }
	    public string AddUrl { get; private set; }

	    public EventListPageModel(BunchContext.Result contextResult, EventList.Result eventListResult)
            : base(contextResult)
	    {
            EventModels = eventListResult.Events.Select(o => new EventListItemModel(o)).ToList();
	        AddUrl = new AddEventUrl(contextResult.Slug).Relative;
	    }

	    public override string BrowserTitle => "Events";
    }
}