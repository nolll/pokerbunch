namespace Core.UseCases
{
    public class UserItem
    {
        public string DisplayName { get; private set; }
        public string UserName { get; private set; }

        public UserItem(string name, string identifier)
        {
            DisplayName = name;
            UserName = identifier;
        }
    }
}