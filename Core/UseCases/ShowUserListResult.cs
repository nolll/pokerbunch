using System.Collections.Generic;

namespace Core.UseCases
{
    public class ShowUserListResult
    {
        public IList<UserItem> Users { get; set; }
    }
}