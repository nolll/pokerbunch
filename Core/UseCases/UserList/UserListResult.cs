using System.Collections.Generic;
using System.Linq;
using Core.Entities;

namespace Core.UseCases.UserList
{
    public class UserListResult
    {
        public IList<UserListItem> Users { get; private set; }

        public UserListResult(IEnumerable<User> users)
        {
            Users = users.Select(o => new UserListItem(o.UserName, o.UserName)).ToList();
        }
    }
}