using System.Collections.Generic;
using Core.Classes;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.PlayerModels.Listing{

	public class PlayerListingPageModel : IPageModel {

        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
        public List<PlayerItemModel> PlayerModels { get; set; }
	    public UrlModel AddUrl { get; set; }
	    public bool ShowAddLink { get; set; }
    }

}