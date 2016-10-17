using Core.Repositories;
using Core.Services;
using Infrastructure;
using Infrastructure.Storage;
using Infrastructure.Storage.CachedRepositories;
using Infrastructure.Storage.Repositories;
using Infrastructure.Web;

namespace Plumbing
{
    public class Dependencies
    {
        private readonly SqlServerStorageProvider _db;
        private readonly ApiConnection _apiConnection;
        private ILocationRepository _locationRepository;
        private IBunchRepository _bunchRepository;
        private AppService _appService;
        private CashgameService _cashgameService;
        private EventService _eventService;
        private PlayerService _playerService;
        private UserService _userService;
        private AuthService _authService;
        private IRandomService _randomService;
        private IMessageSender _messageSender;
                
        public Dependencies(ICacheContainer cacheContainer, string connectionString, string apiUrl, string apiKey, string apiToken)
        {
            Cache = cacheContainer;
            _db = new SqlServerStorageProvider(connectionString);
            _apiConnection = new ApiConnection(apiUrl, apiKey, apiToken);
        }

        public ILocationRepository LocationRepository => _locationRepository ?? (_locationRepository = new ApiLocationRepository(_apiConnection));
        public IBunchRepository BunchRepository => _bunchRepository ?? (_bunchRepository = new ApiBunchRepository(_apiConnection));
        public AppService AppService => _appService ?? (_appService = new AppService(new CachedAppRepository(new SqlAppRepository(_db), Cache)));
        public CashgameService CashgameService => _cashgameService ?? (_cashgameService = new CashgameService(new CachedCashgameRepository(new SqlCashgameRepository(_db), Cache)));
        public EventService EventService => _eventService ?? (_eventService = new EventService(new CachedEventRepository(new SqlEventRepository(_db), Cache)));
        public PlayerService PlayerService => _playerService ?? (_playerService = new PlayerService(new CachedPlayerRepository(new SqlPlayerRepository(_db), Cache)));
        public UserService UserService => _userService ?? (_userService = new UserService(new CachedUserRepository(new SqlUserRepository(_db), Cache)));
        public AuthService AuthService => _authService ?? (_authService = new AuthService(new ApiTokenRepository(_apiConnection)));
        public IRandomService RandomService => _randomService ?? (_randomService = new RandomService());
        public IMessageSender MessageSender => _messageSender ?? (_messageSender = new MessageSender());
        public ICacheContainer Cache { get; }
    }
}