using System.Collections.Generic;

namespace Core.UseCases
{
    public class ShowUserListResult
    {
        public IList<UserListItem> Users { get; set; }
    }
}