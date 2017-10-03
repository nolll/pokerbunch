using Infrastructure.Api.Connection;

namespace Infrastructure.Api.Clients
{
    public class PokerBunchClient
    {
        public AdminClient Admin { get; }
        public AuthClient Auth { get; }
        public UsersClient Users { get; }

        public PokerBunchClient(ApiConnection apiConnection)
        {
            Admin = new AdminClient(apiConnection);
            Auth = new AuthClient(apiConnection);
            Users = new UsersClient(apiConnection);
        }
    }
}