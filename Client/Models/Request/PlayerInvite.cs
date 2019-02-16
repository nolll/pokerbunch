namespace PokerBunch.Client.Models.Request
{
    public class PlayerInvite
    {
        public string PlayerId { get; }
        public string Email { get; }

        public PlayerInvite(string playerId, string email)
        {
            PlayerId = playerId;
            Email = email;
        }
    }
}