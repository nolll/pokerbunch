using Core.UseCases;
using Web.Common.Urls.SiteUrls;
using Web.Models.PageBaseModels;

namespace Web.Models.LocationModels.Details
{
	public class LocationDetailsPageModel : BunchPageModel
    {
        public string Name { get; private set; }
	    public string EditUrl { get; private set; }

        public LocationDetailsPageModel(BunchContext.Result contextResult, LocationDetails.Result locationDetails)
            : base(GetBrowserTitle(locationDetails), contextResult)
	    {
            Name = locationDetails.Name;
            EditUrl = new EditLocationUrl(locationDetails.Id).Relative;
	    }

        private static string GetBrowserTitle(LocationDetails.Result locationDetails)
	    {
	        return string.Format("Location - {0}", locationDetails.Name);
	    }
    }
}