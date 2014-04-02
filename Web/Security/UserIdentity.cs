using System.Collections.Generic;

namespace Web.Security
{
    public class UserIdentity
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public bool IsAdmin { get; set; }
        public List<UserBunch> Bunches { get; set; }
    }
}