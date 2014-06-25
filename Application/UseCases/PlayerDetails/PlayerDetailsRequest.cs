namespace Application.UseCases.PlayerDetails
{
    public class PlayerDetailsRequest
    {
        public string Slug { get; private set; }
        public int PlayerId { get; private set; }

        public PlayerDetailsRequest(string slug, int playerId)
        {
            Slug = slug;
            PlayerId = playerId;
        }
    }
}