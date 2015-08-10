using Core.UseCases;

namespace Web.Models.EventModels.List
{
    public class EventListItemModel
    {
        public string Name { get; private set; }
        public string DetailsUrl { get; private set; }
        public string Location { get; private set; }
        public string StartDate { get; private set; }
        public string EndDate { get; private set; }

        public EventListItemModel(EventList.Item item)
        {
            Name = item.Name;
            DetailsUrl = item.EventDetailsUrl.Relative;
            Location = item.Location;
            StartDate = item.StartDate.IsoString;
            EndDate = item.EndDate.IsoString;
        }
    }
}