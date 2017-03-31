using Core.Services;
using Infrastructure;
using Infrastructure.Storage.Services;
using CashgameService = Infrastructure.Storage.Services.CashgameService;

namespace Plumbing
{
    public class Dependencies
    {
        private readonly string _apiUrl;
        private readonly string _apiKey;
        private readonly string _apiToken;
        private ApiConnection _api;

        private ILocationService _locationService;
        private IBunchService _bunchService;
        private IAppService _appService;
        private ICashgameService _cashgameService;
        private IEventService _eventService;
        private IPlayerService _playerService;
        private IUserService _userService;
        private IAuthService _authService;
        private IAdminService _adminService;
                
        public Dependencies(string apiUrl, string apiKey, string apiToken)
        {
            _apiUrl = apiUrl;
            _apiKey = apiKey;
            _apiToken = apiToken;
        }

        private ApiConnection Api => _api ?? (_api = new ApiConnection(_apiUrl, _apiKey, _apiToken));

        public ILocationService LocationService => _locationService ?? (_locationService = new LocationService(Api));
        public IBunchService BunchService => _bunchService ?? (_bunchService = new BunchService(Api));
        public IAppService AppService => _appService ?? (_appService = new AppService(Api));
        public ICashgameService CashgameService => _cashgameService ?? (_cashgameService = new CashgameService(Api));
        public IEventService EventService => _eventService ?? (_eventService = new EventService(Api));
        public IPlayerService PlayerService => _playerService ?? (_playerService = new PlayerService(Api));
        public IUserService UserService => _userService ?? (_userService = new UserService(Api));
        public IAuthService AuthService => _authService ?? (_authService = new AuthService(Api));
        public IAdminService AdminService => _adminService ?? (_adminService = new AdminService(Api));
    }
}