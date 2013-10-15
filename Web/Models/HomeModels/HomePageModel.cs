using Web.Models.NavigationModels;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.HomeModels{

	public class HomePageModel : IPageModel {

	    public string BrowserTitle { get; set; }
	    public PageProperties PageProperties { get; set; }
	    public bool IsLoggedIn { get; set; }
	    public UrlModel AddHomegameUrl { get; set; }
        public string LoginUrl { get; set; }
        public string RegisterUrl { get; set; }
	    public NavigationModel AdminNav { get; set; }

	}

}