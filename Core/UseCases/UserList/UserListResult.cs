using System.Collections.Generic;

namespace Core.UseCases.UserList
{
    public class UserListResult
    {
        public IList<UserListItem> Users { get; set; }
    }
}