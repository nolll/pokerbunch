namespace Core.UseCases.PlayerList
{
    public class PlayerListRequest
    {
        public string Slug { get; private set; }

        public PlayerListRequest(string slug)
        {
            Slug = slug;
        }
    }
}