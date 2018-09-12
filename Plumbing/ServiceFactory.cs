using Core.Services;
using Infrastructure.Api.FakeServices;
using Infrastructure.Api.Services;
using PokerBunch.Client.Clients;
using CashgameService = Infrastructure.Api.Services.CashgameService;

namespace Plumbing
{
    public class ServiceFactory
    {
        private readonly PokerBunchClient _apiClient;
        private readonly bool _useFakeData;

        public ServiceFactory(PokerBunchClient apiClient, bool useFakeData)
        {
            _apiClient = apiClient;
            _useFakeData = useFakeData;
        }

        public ILocationService Location => _useFakeData ? (ILocationService)new FakeLocationService() : new LocationService(_apiClient);
        public IBunchService Bunch => _useFakeData ? (IBunchService)new FakeBunchService() : new BunchService(_apiClient);
        public IAppService App => _useFakeData ? (IAppService)new FakeAppService() : new AppService(_apiClient);
        public ICashgameService Cashgame => _useFakeData ? (ICashgameService)new FakeCashgameService() : new CashgameService(_apiClient);
        public IEventService Event => _useFakeData ? (IEventService)new FakeEventService() : new EventService(_apiClient);
        public IPlayerService Player => _useFakeData ? (IPlayerService)new FakePlayerService() : new PlayerService(_apiClient);
        public IUserService User => _useFakeData ? (IUserService)new FakeUserService() : new UserService(_apiClient);
        public IAuthService Auth => _useFakeData ? (IAuthService)new FakeAuthService() : new AuthService(_apiClient);
        public IAdminService Admin => _useFakeData ? (IAdminService)new FakeAdminService() : new AdminService(_apiClient);

    }
}