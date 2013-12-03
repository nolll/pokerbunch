using System.Collections.Generic;
using Web.Models.PageBaseModels;

namespace Web.Models.PlayerModels.List{

	public class PlayerListPageModel : IPageModel {

        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
        public List<PlayerItemModel> PlayerModels { get; set; }
	    public string AddUrl { get; set; }
	    public bool ShowAddLink { get; set; }
    }

}