namespace Core.UseCases.PlayerFacts
{
    public class PlayerFactsRequest
    {
        public string Slug { get; private set; }
        public int PlayerId { get; private set; }

        public PlayerFactsRequest(string slug, int playerId)
        {
            Slug = slug;
            PlayerId = playerId;
        }
    }
}