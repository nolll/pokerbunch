using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Join{

	public class JoinHomegameConfirmationPageModel : IPageModel {

	    public string BrowserTitle { get; set; }
	    public PageProperties PageProperties { get; set; }
	    public string BunchName { get; set; }
	    public string BunchUrl { get; set; }
	}

}