using Web.Models.PageBaseModels;

namespace Web.Models.SharingModels{

	public class SharingIndexPageModel : IPageModel
    {
        public PageProperties PageProperties { get; set; }
	    public string BrowserTitle { get; set; }
		public bool IsSharingToTwitter { get; set; }
		public string ShareToTwitterSettingsUrl { get; set; }
	}

}