using Web.Models.MiscModels;
using Web.Models.NavigationModels;

namespace Web.Models.PageBaseModels
{
    public sealed class PageProperties
    {
        public UserNavigationModel UserNavModel { get; set; }
        public HomegameNavigationModel HomegameNavModel { get; set; }
	    public GoogleAnalyticsModel GoogleAnalyticsModel { get; set; }
	}

}