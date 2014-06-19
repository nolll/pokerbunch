using System.Collections.Generic;
using System.Linq;
using Application.UseCases.AppContext;
using Application.UseCases.UserList;
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
            AppContextResult appContextResult,
            UserListResult userListResult)
	    {
            BrowserTitle = "Users";
            PageProperties = new PageProperties(appContextResult);
            UserModels = userListResult.Users.Select(o => new UserListItemModel(o)).ToList();
	    }
    }
}