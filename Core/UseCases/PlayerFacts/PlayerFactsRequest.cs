namespace Core.UseCases.PlayerFacts
{
    public class PlayerFactsRequest
    {
        public int PlayerId { get; private set; }

        public PlayerFactsRequest(int playerId)
        {
            PlayerId = playerId;
        }
    }
}