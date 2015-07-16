namespace Core.UseCases.PlayerList
{
    public class PlayerListRequest
    {
        public string Slug { get; private set; }
        public string UserName { get; private set; }

        public PlayerListRequest(string slug, string userName)
        {
            Slug = slug;
            UserName = userName;
        }
    }
}