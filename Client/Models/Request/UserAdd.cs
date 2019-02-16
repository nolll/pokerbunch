namespace PokerBunch.Client.Models.Request
{
    public class UserAdd
    {
        public string UserName { get; }
        public string DisplayName { get; }
        public string Email { get; }
        public string Password { get; }

        public UserAdd(string userName, string displayName, string email, string password)
        {
            UserName = userName;
            DisplayName = displayName;
            Email = email;
            Password = password;
        }
    }
}