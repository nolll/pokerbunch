using Web.Models.MiscModels;
using Web.Models.NavigationModels;

namespace Web.Models.PageBaseModels
{
    public sealed class PageProperties
    {
        public NavigationModel UserNavModel { get; set; }
        public HomegameNavigationModel HomegameNavModel { get; set; }
	    public GoogleAnalyticsModel GoogleAnalyticsModel { get; set; }
        public string Version { get; set; }
        public string CssUrl { get; set; }
    }

}