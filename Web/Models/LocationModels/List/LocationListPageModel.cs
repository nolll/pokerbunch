using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using Web.Models.PageBaseModels;
using Web.Urls.SiteUrls;

namespace Web.Models.LocationModels.List
{
	public class LocationListPageModel : BunchPageModel
    {
        public IList<LocationListItemModel> LocationModels { get; private set; }
	    public string AddUrl { get; private set; }

        public LocationListPageModel(BunchContext.Result contextResult, LocationList.Result locationListResult)
            : base(contextResult)
	    {
            LocationModels = locationListResult.Events.Select(o => new LocationListItemModel(o)).ToList();
            AddUrl = new AddLocationUrl(contextResult.Slug).Relative;
	    }

	    public override string BrowserTitle => "Locations";
    }
}