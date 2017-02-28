using Core.Repositories;
using Core.Services;
using Infrastructure;
using Infrastructure.Storage;
using Infrastructure.Storage.Repositories;
using Infrastructure.Storage.Services;
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
        private IAppRepository _appRepository;
        private ICashgameRepository _cashgameRepository;
        private IEventRepository _eventRepository;
        private IPlayerRepository _playerRepository;
        private IUserRepository _userRepository;
        private ITokenRepository _tokenRepository;
        private IRandomService _randomService;
        private IMessageSender _messageSender;
        private IAdminService _adminService;
                
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
        public IAppRepository AppRepository => _appRepository ?? (_appRepository = new ApiAppRepository(Api));
        public ICashgameRepository CashgameRepository => _cashgameRepository ?? (_cashgameRepository = new CashgameRepository(Api, Db, Cache));
        public IEventRepository EventRepository => _eventRepository ?? (_eventRepository = new EventRepository(Api, Db, Cache));
        public IPlayerRepository PlayerRepository => _playerRepository ?? (_playerRepository = new PlayerRepository(Api, Db, Cache));
        public IUserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(Api, Db, Cache));
        public ITokenRepository TokenRepository => _tokenRepository ?? (_tokenRepository = new ApiTokenRepository(Api));
        public IRandomService RandomService => _randomService ?? (_randomService = new RandomService());
        public IMessageSender MessageSender => _messageSender ?? (_messageSender = new MessageSender());
        public IAdminService AdminService => _adminService ?? (_adminService = new AdminService(Api));
        public ICacheContainer Cache { get; }
    }
}