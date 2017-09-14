using Core.UseCases;
using Web.Extensions;
using Web.Urls.SiteUrls;

namespace Web.Models.LocationModels.List
{
    public class LocationListItemModel : IViewModel
    {
        public string Name { get; private set; }
        public string DetailsUrl { get; private set; }

        public LocationListItemModel(LocationList.Item item)
        {
            Name = item.Name;
            DetailsUrl = new LocationDetailsUrl(item.LocationId).Relative;
        }

        public View GetView()
        {
            return new View("LocationList/Item");
        }
    }
}