using System.Collections.Generic;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.List
{
	public class UserListPageModel : IPageModel
    {
        public PageProperties PageProperties { get; private set; }
        public string BrowserTitle { get; private set; }
        public IList<UserListItemModel> UserModels { get; private set; }

	    public UserListPageModel()
	    {
	    }

	    public UserListPageModel(
            string browserTitle, 
            PageProperties pageProperties,
            IList<UserListItemModel> userModels)
	    {
            BrowserTitle = browserTitle;
            PageProperties = pageProperties;
	        UserModels = userModels;
	    }
    }
}