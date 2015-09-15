using Core.Repositories;
using Core.Services;
using Infrastructure.Storage;
using Infrastructure.Storage.Repositories;
using Infrastructure.Web;

namespace Plumbing
{
    public class Dependencies
    {
        private readonly IRepositoryFactory _cachedRepositoryFactory;
        private readonly SqlServerStorageProvider _db;
        private AppService _appService;
        private BunchService _bunchService;
        private CashgameService _cashgameService;
        private EventService _eventService;
        private PlayerService _playerService;
        private UserService _userService;
        private IRandomService _randomService;
        private IMessageSender _messageSender;
                
        public Dependencies(IRepositoryFactory cachedRepositoryFactory)
        {
            _cachedRepositoryFactory = cachedRepositoryFactory;
            _db = new SqlServerStorageProvider();
        }

        public AppService AppService
        {
            get { return _appService ?? (_appService = new AppService(_cachedRepositoryFactory.CreateAppRepository(new SqlAppRepository(_db)))); }
        }

        public BunchService BunchService
        {
            get { return _bunchService ?? (_bunchService = new BunchService(_cachedRepositoryFactory.CreateBunchRepository(new SqlBunchRepository(_db)))); }
        }

        public CashgameService CashgameService
        {
            get { return _cashgameService ?? (_cashgameService = new CashgameService(_cachedRepositoryFactory.CreateCashgameRepository(new SqlCashgameRepository(_db)))); }
        }

        public EventService EventService
        {
            get { return _eventService ?? (_eventService = new EventService(_cachedRepositoryFactory.CreateEventRepository(new SqlEventRepository(_db)))); }
        }

        public PlayerService PlayerService
        {
            get { return _playerService ?? (_playerService = new PlayerService(_cachedRepositoryFactory.CreatePlayerRepository(new SqlPlayerRepository(_db)))); }
        }

        public UserService UserService
        {
            get { return _userService ?? (_userService = new UserService(_cachedRepositoryFactory.CreateUserRepository(new SqlUserRepository(_db)))); }
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