using Core.UseCases.BunchContext;
using Core.UseCases.EventDetails;
using Web.Models.PageBaseModels;

namespace Web.Models.EventModels.Details
{
	public class EventDetailsPageModel : BunchPageModel
    {
        public string Name { get; private set; }

	    public EventDetailsPageModel(BunchContextResult contextResult, EventDetailsOutput eventDetailsOutput)
            : base(GetBrowserTitle(eventDetailsOutput), contextResult)
	    {
            Name = eventDetailsOutput.Name;
	    }

	    private static string GetBrowserTitle(EventDetailsOutput eventDetailsOutput)
	    {
	        return string.Format("Event - {0}", eventDetailsOutput.Name);
	    }
    }
}