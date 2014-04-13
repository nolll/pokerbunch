namespace Core.UseCases.UserList
{
    public class UserListItem
    {
        public string DisplayName { get; private set; }
        public string UserName { get; private set; }

        public UserListItem()
        {
        }

        public UserListItem(string name, string identifier)
        {
            DisplayName = name;
            UserName = identifier;
        }
    }
}