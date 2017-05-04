namespace Core.Entities
{
    public class ListUser
    {
        public string UserName { get; private set; }
        public string DisplayName { get; private set; }

        public ListUser(
            string userName,
            string displayName = null)
        {
            UserName = userName;
            DisplayName = displayName ?? string.Empty;
        }
    }
}