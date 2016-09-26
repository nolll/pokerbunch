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
        private readonly ICacheContainer _cacheContainer;
        private readonly SqlServerStorageProvider _db;
        private readonly ApiConnection _apiConnection;
        private AppService _appService;
        private BunchService _bunchService;
        private CashgameService _cashgameService;
        private EventService _eventService;
        private PlayerService _playerService;
        private LocationService _locationService;
        private UserService _userService;
        private IRandomService _randomService;
        private IMessageSender _messageSender;
                
        public Dependencies(ICacheContainer cacheContainer, string connectionString, string apiHost, string apiUrl, string apiKey, string apiUsername, string apiPassword)
        {
            _cacheContainer = cacheContainer;
            _db = new SqlServerStorageProvider(connectionString);
            _apiConnection = ApiConnection.GetInstance(apiHost, apiUrl, apiKey, apiUsername, apiPassword);
        }

        public AppService AppService => _appService ?? (_appService = new AppService(new CachedAppRepository(new SqlAppRepository(_db), _cacheContainer)));
        public BunchService BunchService => _bunchService ?? (_bunchService = new BunchService(new CachedBunchRepository(new SqlBunchRepository(_db), _cacheContainer)));
        public CashgameService CashgameService => _cashgameService ?? (_cashgameService = new CashgameService(new CachedCashgameRepository(new SqlCashgameRepository(_db), _cacheContainer)));
        public EventService EventService => _eventService ?? (_eventService = new EventService(new CachedEventRepository(new SqlEventRepository(_db), _cacheContainer)));
        public PlayerService PlayerService => _playerService ?? (_playerService = new PlayerService(new CachedPlayerRepository(new SqlPlayerRepository(_db), _cacheContainer)));
        public LocationService LocationService => _locationService ?? (_locationService = new LocationService(new ApiLocationRepository(_apiConnection)));
        public UserService UserService => _userService ?? (_userService = new UserService(new CachedUserRepository(new SqlUserRepository(_db), _cacheContainer)));
        public IRandomService RandomService => _randomService ?? (_randomService = new RandomService());
        public IMessageSender MessageSender => _messageSender ?? (_messageSender = new MessageSender());
    }
}