using System.Collections.Generic;
using System.Linq;
using Core.Settings;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.LocationModels.List
{
	public class LocationListPageModel : BunchPageModel
    {
        public IList<LocationListItemModel> LocationModels { get; }
	    public string AddUrl { get; }

        public LocationListPageModel(AppSettings appSettings, BunchContext.Result contextResult, LocationList.Result locationListResult)
            : base(appSettings, contextResult)
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