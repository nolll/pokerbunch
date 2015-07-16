namespace Core.UseCases.PlayerDetails
{
    public class PlayerDetailsRequest
    {
        public string Slug { get; private set; }
        public int PlayerId { get; private set; }
        public string UserName { get; private set; }

        public PlayerDetailsRequest(string slug, int playerId, string userName)
        {
            Slug = slug;
            PlayerId = playerId;
            UserName = userName;
        }
    }
}