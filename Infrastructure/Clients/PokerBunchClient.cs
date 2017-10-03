using Infrastructure.Api.Connection;

namespace Infrastructure.Api.Clients
{
    public class PokerBunchClient
    {
        public AdminClient Admin { get; }
        public AuthClient Auth { get; }
        public EventClient Events { get; }
        public LocationClient Locations { get; }
        public PlayerClient Players { get; }
        public UserClient Users { get; }

        public PokerBunchClient(ApiConnection apiConnection)
        {
            Admin = new AdminClient(apiConnection);
            Auth = new AuthClient(apiConnection);
            Events = new EventClient(apiConnection);
            Locations = new LocationClient(apiConnection);
            Players = new PlayerClient(apiConnection);
            Users = new UserClient(apiConnection);
        }
    }
}