using System.Collections.Generic;
using System.Linq;
using Core.Entities;

namespace Application.UseCases.UserList
{
    public class UserListResult
    {
        public IList<UserListItem> Users { get; set; }

        public UserListResult(IList<User> users)
        {
            Users = users.Select(o => new UserListItem(o.UserName, o.UserName)).ToList();
        }

        protected UserListResult(IList<UserListItem> userListItems)
        {
            Users = userListItems;
        }
    }

    public class UserListResultInTest : UserListResult
    {
        public UserListResultInTest(
            IList<UserListItem> userListItems = null)
            
            : base(
            userListItems ?? new List<UserListItem>())
        {
        }
    }
}