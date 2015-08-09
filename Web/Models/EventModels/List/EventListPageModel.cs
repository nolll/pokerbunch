using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using Core.UseCases.EventList;
using Web.Models.PageBaseModels;

namespace Web.Models.EventModels.List
{
	public class EventListPageModel : BunchPageModel
    {
        public IList<EventListItemModel> EventModels { get; private set; }

	    public EventListPageModel(BunchContext.Result contextResult, EventListOutput eventListOutput)
            : base("Events", contextResult)
	    {
            EventModels = eventListOutput.Events.Select(o => new EventListItemModel(o)).ToList();
	    }
    }
}