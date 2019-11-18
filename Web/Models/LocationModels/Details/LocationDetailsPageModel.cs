using Core.Settings;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.LocationModels.Details
{
	public class LocationDetailsPageModel : BunchPageModel
    {
	    private readonly LocationDetails.Result _locationDetails;
	    public string Name { get; }
	    public string EditUrl { get; }

        public LocationDetailsPageModel(AppSettings appSettings, BunchContext.Result contextResult, LocationDetails.Result locationDetails)
            : base(appSettings, contextResult)
	    {
            _locationDetails = locationDetails;
            Name = locationDetails.Name;
            EditUrl = new EditLocationUrl(locationDetails.Id).Relative;
	    }

	    public override string BrowserTitle => $"Location - {_locationDetails.Name}";

	    public override View GetView()
	    {
	        return new View("~/Views/Pages/LocationDetails/LocationDetails.cshtml");
	    }
    }
}