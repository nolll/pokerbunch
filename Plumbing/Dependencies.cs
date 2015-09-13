using Core.Repositories;
using Core.Services;
using Infrastructure.Storage;
using Infrastructure.Storage.Interfaces;
using Infrastructure.Storage.Repositories;
using Infrastructure.Web;

namespace Plumbing
{
    public class Dependencies
    {
        private readonly IRepositoryFactory _cachedRepositoryFactory;
        private readonly SqlServerStorageProvider _db;

        public Dependencies(IRepositoryFactory cachedRepositoryFactory)
        {
            _cachedRepositoryFactory = cachedRepositoryFactory;
            _db = new SqlServerStorageProvider();
        }

        private AppService _appService;
        public AppService AppService
        {
            get { return _appService ?? (_appService = new AppService(_cachedRepositoryFactory.GetAppRepository(new SqlAppRepository(_db)))); }
        }

        private BunchService _bunchService;
        public BunchService BunchService
        {
            get { return _bunchService ?? (_bunchService = new BunchService(_cachedRepositoryFactory.GetBunchRepository(new SqlBunchRepository(_db)))); }
        }

        private CashgameService _cashgameService;
        public CashgameService CashgameService
        {
            get { return _cashgameService ?? (_cashgameService = new CashgameService(_cachedRepositoryFactory.GetCashgameRepository(new SqlCashgameRepository(CashgameStorage, CheckpointStorage)))); }
        }

        private CheckpointService _checkpointService;
        public CheckpointService CheckpointService
        {
            get { return _checkpointService ?? (_checkpointService = new CheckpointService(CheckpointRepository)); }
        }

        private EventService _eventService;
        public EventService EventService
        {
            get { return _eventService ?? (_eventService = new EventService(_cachedRepositoryFactory.GetEventRepository(new SqlEventRepository(_db)))); }
        }

        private PlayerService _playerService;
        public PlayerService PlayerService
        {
            get { return _playerService ?? (_playerService = new PlayerService(PlayerRepository)); }
        }

        private UserService _userService;
        public UserService UserService
        {
            get { return _userService ?? (_userService = new UserService(UserRepository)); }
        }

        private IRandomService _randomService;
        public IRandomService RandomService
        {
            get { return _randomService ?? (_randomService = new RandomService()); }
        }

        private IMessageSender _messageSender;
        public IMessageSender MessageSender
        {
            get { return _messageSender ?? (_messageSender = new MessageSender()); }
        }
        
        private IUserRepository _userRepository;
        private IUserRepository UserRepository
        {
            get { return _userRepository ?? (_userRepository = _cachedRepositoryFactory.GetUserRepository(new SqlUserRepository(_db))); }
        }

        private IPlayerRepository _playerRepository;
        private IPlayerRepository PlayerRepository
        {
            get { return _playerRepository ?? (_playerRepository = _cachedRepositoryFactory.GetPlayerRepository(new SqlPlayerRepository(PlayerStorage, UserRepository))); }
        }
        
        private ICheckpointRepository _checkpointRepository;
        public ICheckpointRepository CheckpointRepository
        {
            get { return _checkpointRepository ?? (_checkpointRepository = _cachedRepositoryFactory.GetCheckpointRepository(new SqlCheckpointRepository(CheckpointStorage))); }
        }
        
        private IPlayerStorage _playerStorage;
        private IPlayerStorage PlayerStorage
        {
            get { return _playerStorage ?? (_playerStorage = new SqlServerPlayerStorage()); }
        }

        private ICheckpointStorage _checkpointStorage;
        private ICheckpointStorage CheckpointStorage
        {
            get { return _checkpointStorage ?? (_checkpointStorage = new SqlServerCheckpointStorage()); }
        }

        private ICashgameStorage _cashgameStorage;
        private ICashgameStorage CashgameStorage
        {
            get { return _cashgameStorage ?? (_cashgameStorage = new SqlServerCashgameStorage()); }
        }
    }
}