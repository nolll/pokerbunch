using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;

namespace Web.Models.EventModels.List
{
    public class EventListItemModel : IViewModel
    {
        public string Name { get; }
        public string DetailsUrl { get; }
        public string TimeAndLocation { get; }

        public EventListItemModel(EventList.Item item)
        {
            Name = item.Name;
            DetailsUrl = new EventDetailsUrl(item.EventId).Relative;
            TimeAndLocation = item.HasGames ? string.Format("{0}, {1}", item.Location, item.StartDate.IsoString) : "This event has no games";
        }

        public View GetView()
        {
            return new View("~/Views/Pages/EventList/Item.cshtml");
        }
    }
}