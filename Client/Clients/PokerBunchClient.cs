using PokerBunch.Client.Connection;

namespace PokerBunch.Client.Clients
{
    public class PokerBunchClient
    {
        public AuthClient Auth { get; }
        public BunchClient Bunches { get; }
        public PlayerClient Players { get; }
        public UserClient Users { get; }

        public PokerBunchClient(ApiConnection apiConnection)
        {
            Auth = new AuthClient(apiConnection);
            Bunches = new BunchClient(apiConnection);
            Players = new PlayerClient(apiConnection);
            Users = new UserClient(apiConnection);
        }
    }
}