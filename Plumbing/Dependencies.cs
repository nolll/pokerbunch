using Core.Services;
using PokerBunch.Client.Clients;
using PokerBunch.Client.Connection;

namespace Plumbing
{
    public class Dependencies
    {
        private readonly string _apiHost;
        private readonly string _apiProtocol;
        private readonly string _apiKey;
        private readonly string _apiToken;
        private readonly bool _isDetailedErrorMessagesEnabled;
        private readonly bool _useFakeData;

        private ApiConnection _api;
        private PokerBunchClient _apiClient;
        private ServiceFactory _serviceFactory;

        private ILocationService _locationService;
        private IBunchService _bunchService;
        private IAppService _appService;
        private ICashgameService _cashgameService;
        private IEventService _eventService;
        private IPlayerService _playerService;
        private IUserService _userService;
        private IAuthService _authService;
        private IAdminService _adminService;
                
        public Dependencies(
            string apiHost, 
            string apiProtocol, 
            string apiKey, 
            string apiToken, 
            bool isDetailedErrorMessagesEnabled,
            bool useFakeData)
        {
            _apiHost = apiHost;
            _apiProtocol = apiProtocol;
            _apiKey = apiKey;
            _apiToken = apiToken;
            _isDetailedErrorMessagesEnabled = isDetailedErrorMessagesEnabled;
            _useFakeData = useFakeData;
        }

        private ApiConnection Api => _api ?? (_api = new ApiConnection(_apiHost, _apiProtocol, _apiKey, _apiToken, _isDetailedErrorMessagesEnabled));
        private PokerBunchClient ApiClient => _apiClient ?? (_apiClient = new PokerBunchClient(Api));
        private ServiceFactory Services => _serviceFactory ?? (_serviceFactory = new ServiceFactory(ApiClient, _useFakeData));

        public ILocationService LocationService => _locationService ?? (_locationService = Services.Location);
        public IBunchService BunchService => _bunchService ?? (_bunchService = Services.Bunch);
        public IAppService AppService => _appService ?? (_appService = Services.App);
        public ICashgameService CashgameService => _cashgameService ?? (_cashgameService = Services.Cashgame);
        public IEventService EventService => _eventService ?? (_eventService = Services.Event);
        public IPlayerService PlayerService => _playerService ?? (_playerService = Services.Player);
        public IUserService UserService => _userService ?? (_userService = Services.User);
        public IAuthService AuthService => _authService ?? (_authService = Services.Auth);
        public IAdminService AdminService => _adminService ?? (_adminService = Services.Admin);
    }
}