using Core.Services;
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
        private AppService _appService;
        private BunchService _bunchService;
        private CashgameService _cashgameService;
        private EventService _eventService;
        private PlayerService _playerService;
        private UserService _userService;
        private IRandomService _randomService;
        private IMessageSender _messageSender;
                
        public Dependencies(ICacheContainer cacheContainer)
        {
            _cacheContainer = cacheContainer;
            _db = new SqlServerStorageProvider();
        }

        public AppService AppService
        {
            get { return _appService ?? (_appService = new AppService(new CachedAppRepository(new SqlAppRepository(_db), _cacheContainer))); }
        }

        public BunchService BunchService
        {
            get { return _bunchService ?? (_bunchService = new BunchService(new CachedBunchRepository(new SqlBunchRepository(_db), _cacheContainer))); }
        }

        public CashgameService CashgameService
        {
            get { return _cashgameService ?? (_cashgameService = new CashgameService(new CachedCashgameRepository(new SqlCashgameRepository(_db), _cacheContainer))); }
        }

        public EventService EventService
        {
            get { return _eventService ?? (_eventService = new EventService(new CachedEventRepository(new SqlEventRepository(_db), _cacheContainer))); }
        }

        public PlayerService PlayerService
        {
            get { return _playerService ?? (_playerService = new PlayerService(new CachedPlayerRepository(new SqlPlayerRepository(_db), _cacheContainer))); }
        }

        public UserService UserService
        {
            get { return _userService ?? (_userService = new UserService(new CachedUserRepository(new SqlUserRepository(_db), _cacheContainer))); }
        }

        public IRandomService RandomService
        {
            get { return _randomService ?? (_randomService = new RandomService()); }
        }

        public IMessageSender MessageSender
        {
            get { return _messageSender ?? (_messageSender = new MessageSender()); }
        }
    }
}