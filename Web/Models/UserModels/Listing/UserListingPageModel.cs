using System.Collections.Generic;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.Listing{

	public class UserListingPageModel : IPageModel {

        public PageProperties PageProperties { get; set; }
	    public string BrowserTitle { get; set; }
	    public List<UserItemModel> UserModels { get; set; }

	}

}