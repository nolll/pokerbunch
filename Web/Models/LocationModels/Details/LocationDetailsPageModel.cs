using Core.UseCases;
using Web.Models.PageBaseModels;
using Web.Urls.SiteUrls;

namespace Web.Models.LocationModels.Details
{
	public class LocationDetailsPageModel : BunchPageModel
    {
	    private readonly LocationDetails.Result _locationDetails;
	    public string Name { get; private set; }
	    public string EditUrl { get; private set; }

        public LocationDetailsPageModel(BunchContext.Result contextResult, LocationDetails.Result locationDetails)
            : base(contextResult)
	    {
            _locationDetails = locationDetails;
            Name = locationDetails.Name;
            EditUrl = new EditLocationUrl(locationDetails.Id).Relative;
	    }

	    public override string BrowserTitle => $"Location - {_locationDetails.Name}";
    }
}