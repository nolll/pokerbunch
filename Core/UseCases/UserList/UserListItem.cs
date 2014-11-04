namespace Core.UseCases.UserList
{
    public class UserListItem
    {
        public string DisplayName { get; private set; }
        public string UserName { get; private set; }

        public UserListItem(string displayName, string userName)
        {
            DisplayName = displayName;
            UserName = userName;
        }
    }
}