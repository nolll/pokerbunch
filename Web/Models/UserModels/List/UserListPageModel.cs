using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.UserModels.List
{
    public class UserListPageModel : AppPageModel
    {
        public IList<UserListItemModel> UserModels { get; }

        public UserListPageModel(CoreContext.Result contextResult, UserList.Result userListResult)
            : base(contextResult)
        {
            UserModels = userListResult.Users.Select(o => new UserListItemModel(o)).ToList();
        }

        public override string BrowserTitle => "Users";

        public override View GetView()
        {
            return new View("~/Views/Pages/UserList/UserList.cshtml");
        }
    }
}