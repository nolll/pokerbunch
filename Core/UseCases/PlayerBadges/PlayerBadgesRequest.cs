namespace Core.UseCases.PlayerBadges
{
    public class PlayerBadgesRequest
    {
        public string Slug { get; private set; }
        public int PlayerId { get; private set; }

        public PlayerBadgesRequest(string slug, int playerId)
        {
            Slug = slug;
            PlayerId = playerId;
        }
    }
}