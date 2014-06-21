namespace Application.UseCases.PlayerList
{
    public class PlayerListRequest
    {
        public string Slug;

        public PlayerListRequest(string slug)
        {
            Slug = slug;
        }
    }
}