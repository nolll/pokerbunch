using System.Collections.Generic;

namespace Core.UseCases.ShowUserList
{
    public class ShowUserListResult
    {
        public IList<UserListItem> Users { get; set; }
    }
}