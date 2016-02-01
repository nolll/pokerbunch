using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.List
{
    public class UserListPageModel : AppPageModel
    {
        public IList<UserListItemModel> UserModels { get; private set; }

        public UserListPageModel(CoreContext.Result contextResult, UserList.Result userListResult)
            : base("Users", contextResult)
        {
            UserModels = userListResult.Users.Select(o => new UserListItemModel(o)).ToList();
        }
    }
}