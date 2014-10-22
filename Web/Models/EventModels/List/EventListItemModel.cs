using Core.UseCases.EventList;

namespace Web.Models.EventModels.List
{
    public class EventListItemModel
    {
        public string Name { get; private set; }
        public string DetailsUrl { get; private set; }

        public EventListItemModel(EventItem eventItem)
        {
            Name = eventItem.Name;
            DetailsUrl = eventItem.EventDetailsUrl.Relative;
        }
    }
}