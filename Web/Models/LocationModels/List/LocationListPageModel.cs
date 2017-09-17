using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;
using Web.Urls.SiteUrls;

namespace Web.Models.LocationModels.List
{
	public class LocationListPageModel : BunchPageModel
    {
        public IList<LocationListItemModel> LocationModels { get; }
	    public string AddUrl { get; }

        public LocationListPageModel(BunchContext.Result contextResult, LocationList.Result locationListResult)
            : base(contextResult)
	    {
            LocationModels = locationListResult.Events.Select(o => new LocationListItemModel(o)).ToList();
            AddUrl = new AddLocationUrl(contextResult.Slug).Relative;
	    }

	    public override string BrowserTitle => "Locations";

	    public override View GetView()
	    {
	        return new View("~/Views/Pages/LocationList/LocationList.cshtml");
	    }
    }
}