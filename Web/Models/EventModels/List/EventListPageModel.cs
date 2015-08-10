using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.EventModels.List
{
	public class EventListPageModel : BunchPageModel
    {
        public IList<EventListItemModel> EventModels { get; private set; }

	    public EventListPageModel(BunchContext.Result contextResult, EventList.Result eventListResult)
            : base("Events", contextResult)
	    {
            EventModels = eventListResult.Events.Select(o => new EventListItemModel(o)).ToList();
	    }
    }
}