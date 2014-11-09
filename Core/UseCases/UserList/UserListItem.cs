using Core.Urls;

namespace Core.UseCases.UserList
{
    public class UserListItem
    {
        public string DisplayName { get; private set; }
        public Url Url { get; private set; }

        public UserListItem(string displayName, string userName)
        {
            DisplayName = displayName;
            Url = new UserDetailsUrl(userName);
        }
    }
}