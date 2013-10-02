using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.SharingModels{

	public class SharingIndexPageModel : IPageModel
    {
        public PageProperties PageProperties { get; set; }
	    public string BrowserTitle { get; set; }
		public bool IsSharingToTwitter { get; set; }
		public UrlModel ShareToTwitterSettingsUrl { get; set; }
	}

}