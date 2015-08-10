namespace Core.UseCases.PlayerBadges
{
    public class PlayerBadgesRequest
    {
        public int PlayerId { get; private set; }

        public PlayerBadgesRequest(int playerId)
        {
            PlayerId = playerId;
        }
    }
}