namespace PokerBunch.Client.Models.Request
{
    public class UserUpdate
    {
        public string UserName { get; }
        public string DisplayName { get; }
        public string RealName { get; }
        public string Email { get; }

        public UserUpdate(string userName, string displayName, string realName, string email)
        {
            UserName = userName;
            DisplayName = displayName;
            RealName = realName;
            Email = email;
        }
    }
}