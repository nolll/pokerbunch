using Core.UseCases;
using Web.Common.Urls.SiteUrls;

namespace Web.Models.LocationModels.List
{
    public class LocationListItemModel
    {
        public string Name { get; private set; }
        public string DetailsUrl { get; private set; }

        public LocationListItemModel(LocationList.Item item)
        {
            Name = item.Name;
            DetailsUrl = new LocationDetailsUrl(item.LocationId).Relative;
        }
    }
}