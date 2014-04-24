using System.Collections.Generic;

namespace Application.UseCases.UserList
{
    public class UserListResult
    {
        public IList<UserListItem> Users { get; set; }
    }
}