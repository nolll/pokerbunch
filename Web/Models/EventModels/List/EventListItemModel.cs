using Core.UseCases;
using Web.Extensions;
using Web.Urls.SiteUrls;

namespace Web.Models.EventModels.List
{
    public class EventListItemModel : IViewModel
    {
        public string Name { get; private set; }
        public string DetailsUrl { get; private set; }
        public string TimeAndLocation { get; private set; }

        public EventListItemModel(EventList.Item item)
        {
            Name = item.Name;
            DetailsUrl = new EventDetailsUrl(item.EventId).Relative;
            TimeAndLocation = item.HasGames ? string.Format("{0}, {1}", item.Location, item.StartDate.IsoString) : "This event has no games";
        }

        public View GetView()
        {
            return new View("EventList/Item");
        }
    }
}