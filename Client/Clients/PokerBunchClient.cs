using PokerBunch.Client.Connection;
using PokerBunch.Client.Models;

namespace PokerBunch.Client.Clients
{
    public class PokerBunchClient
    {
        public AdminClient Admin { get; }
        public AppClient Apps { get; }
        public AuthClient Auth { get; }
        public BunchClient Bunches { get; }
        public CashgameClient Cashgames { get; }
        public EventClient Events { get; }
        public LocationClient Locations { get; }
        public PlayerClient Players { get; }
        public UserClient Users { get; }

        public PokerBunchClient(ApiConnection apiConnection)
        {
            Admin = new AdminClient(apiConnection);
            Apps = new AppClient(apiConnection);
            Auth = new AuthClient(apiConnection);
            Bunches = new BunchClient(apiConnection);
            Cashgames = new CashgameClient(apiConnection);
            Events = new EventClient(apiConnection);
            Locations = new LocationClient(apiConnection);
            Players = new PlayerClient(apiConnection);
            Users = new UserClient(apiConnection);
        }
    }
}