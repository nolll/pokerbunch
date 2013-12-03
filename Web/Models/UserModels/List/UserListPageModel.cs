using System.Collections.Generic;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.List{

	public class UserListPageModel : IPageModel {

        public PageProperties PageProperties { get; set; }
	    public string BrowserTitle { get; set; }
	    public List<UserItemModel> UserModels { get; set; }

	}

}