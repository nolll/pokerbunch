using System;
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
        private readonly string _connectionString;
        private readonly string _apiUrl;
        private readonly string _apiKey;
        private readonly string _apiToken;
        private SqlServerStorageProvider _db;
        private ApiConnection _api;

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
            _connectionString = connectionString;
            _apiUrl = apiUrl;
            _apiKey = apiKey;
            _apiToken = apiToken;
        }

        private SqlServerStorageProvider Db => _db ?? (_db = new SqlServerStorageProvider(_connectionString));
        private ApiConnection Api => _api ?? (_api = new ApiConnection(_apiUrl, _apiKey, _apiToken));

        public ILocationRepository LocationRepository => _locationRepository ?? (_locationRepository = new ApiLocationRepository(Api));
        public IBunchRepository BunchRepository => _bunchRepository ?? (_bunchRepository = new ApiBunchRepository(Api));
        public AppService AppService => _appService ?? (_appService = new AppService(new CachedAppRepository(new SqlAppRepository(Db), Cache)));
        public CashgameService CashgameService => _cashgameService ?? (_cashgameService = new CashgameService(new CachedCashgameRepository(new SqlCashgameRepository(Db), Cache)));
        public EventService EventService => _eventService ?? (_eventService = new EventService(new CachedEventRepository(new SqlEventRepository(Db), Cache)));
        public PlayerService PlayerService => _playerService ?? (_playerService = new PlayerService(new CachedPlayerRepository(new SqlPlayerRepository(Db), Cache)));
        public UserService UserService => _userService ?? (_userService = new UserService(new CachedUserRepository(new SqlUserRepository(Db), Cache)));
        public AuthService AuthService => _authService ?? (_authService = new AuthService(new ApiTokenRepository(Api)));
        public IRandomService RandomService => _randomService ?? (_randomService = new RandomService());
        public IMessageSender MessageSender => _messageSender ?? (_messageSender = new MessageSender());
        public ICacheContainer Cache { get; }
    }
}