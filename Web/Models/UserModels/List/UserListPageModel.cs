using System.Collections.Generic;
using System.Linq;
using Application.UseCases.AppContext;
using Application.UseCases.UserList;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.List
{
	public class UserListPageModel : PageModel
    {
        public IList<UserListItemModel> UserModels { get; private set; }

	    public UserListPageModel(AppContextResult contextResult, UserListResult userListResult)
            : base("Users", contextResult)
	    {
            UserModels = userListResult.Users.Select(o => new UserListItemModel(o)).ToList();
	    }
    }
}