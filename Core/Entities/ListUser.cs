namespace Core.Entities
{
    public class ListUser
    {
        public string UserName { get; }
        public string DisplayName { get; }

        public ListUser(
            string userName,
            string displayName = null)
        {
            UserName = userName;
            DisplayName = displayName ?? string.Empty;
        }
    }
}