using System.Collections.Generic;
using System.Linq;
using Core.Services;

namespace Core.UseCases
{
    public class UserList
    {
        private readonly IUserService _userService;

        public UserList(IUserService userService)
        {
            _userService = userService;
        }

        public Result Execute()
        {
            var users = _userService.List();
            var userItems = users.Select(o => new UserListItem(o.DisplayName, o.UserName)).ToList();

            return new Result(userItems);
        }

        public class Result
        {
            public IList<UserListItem> Users { get; }

            public Result(IList<UserListItem> userItems)
            {
                Users = userItems;
            }
        }

        public class UserListItem
        {
            public string DisplayName { get; }
            public string UserName { get; }

            public UserListItem(string displayName, string userName)
            {
                DisplayName = displayName;
                UserName = userName;
            }
        }
    }
}