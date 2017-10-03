using Core.Services;
using Infrastructure;
using Infrastructure.Api;
using Infrastructure.Api.Clients;
using Infrastructure.Api.Connection;
using Infrastructure.Api.Services;
using CashgameService = Infrastructure.Api.Services.CashgameService;

namespace Plumbing
{
    public class Dependencies
    {
        private readonly string _apiHost;
        private readonly string _apiProtocol;
        private readonly string _apiKey;
        private readonly string _apiToken;
        private readonly bool _isDetailedErrorMessagesEnabled;
        private ApiConnection _api;
        private PokerBunchClient _apiClient;

        private ILocationService _locationService;
        private IBunchService _bunchService;
        private IAppService _appService;
        private ICashgameService _cashgameService;
        private IEventService _eventService;
        private IPlayerService _playerService;
        private IUserService _userService;
        private IAuthService _authService;
        private IAdminService _adminService;
                
        public Dependencies(string apiHost, string apiProtocol, string apiKey, string apiToken, bool isDetailedErrorMessagesEnabled)
        {
            _apiHost = apiHost;
            _apiProtocol = apiProtocol;
            _apiKey = apiKey;
            _apiToken = apiToken;
            _isDetailedErrorMessagesEnabled = isDetailedErrorMessagesEnabled;
        }

        private ApiConnection Api => _api ?? (_api = new ApiConnection(_apiHost, _apiProtocol, _apiKey, _apiToken, _isDetailedErrorMessagesEnabled));
        private PokerBunchClient ApiClient => _apiClient ?? (_apiClient = new PokerBunchClient(Api));

        public ILocationService LocationService => _locationService ?? (_locationService = new LocationService(ApiClient));
        public IBunchService BunchService => _bunchService ?? (_bunchService = new BunchService(Api));
        public IAppService AppService => _appService ?? (_appService = new AppService(Api));
        public ICashgameService CashgameService => _cashgameService ?? (_cashgameService = new CashgameService(Api));
        public IEventService EventService => _eventService ?? (_eventService = new EventService(Api));
        public IPlayerService PlayerService => _playerService ?? (_playerService = new PlayerService(ApiClient));
        public IUserService UserService => _userService ?? (_userService = new UserService(ApiClient));
        public IAuthService AuthService => _authService ?? (_authService = new AuthService(ApiClient));
        public IAdminService AdminService => _adminService ?? (_adminService = new AdminService(ApiClient));
    }
}