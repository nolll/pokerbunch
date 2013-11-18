using Web.Models.PageBaseModels;

namespace Web.Models.HomegameModels.Join{

	public class JoinHomegamePageModel : JoinHomegamePostModel, IPageModel {

        public PageProperties PageProperties { get; set; }
	    public string BrowserTitle { get; set; }
	    public string Name { get; set; }
	}

}