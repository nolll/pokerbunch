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
        private AppService _appService;
        private BunchService _bunchService;
        private CashgameService _cashgameService;
        private CheckpointService _checkpointService;
        private EventService _eventService;
        private PlayerService _playerService;
        private UserService _userService;
        private IRandomService _randomService;
        private IMessageSender _messageSender;
        private IUserRepository _userRepository;
        private IPlayerRepository _playerRepository;
        private ICheckpointRepository _checkpointRepository;
        private ICheckpointStorage _checkpointStorage;
        private ICashgameStorage _cashgameStorage;
                
        public Dependencies(IRepositoryFactory cachedRepositoryFactory)
        {
            _cachedRepositoryFactory = cachedRepositoryFactory;
            _db = new SqlServerStorageProvider();
        }

        public AppService AppService
        {
            get { return _appService ?? (_appService = new AppService(_cachedRepositoryFactory.GetAppRepository(new SqlAppRepository(_db)))); }
        }

        public BunchService BunchService
        {
            get { return _bunchService ?? (_bunchService = new BunchService(_cachedRepositoryFactory.GetBunchRepository(new SqlBunchRepository(_db)))); }
        }

        public CashgameService CashgameService
        {
            get { return _cashgameService ?? (_cashgameService = new CashgameService(_cachedRepositoryFactory.GetCashgameRepository(new SqlCashgameRepository(CashgameStorage, CheckpointStorage)), CheckpointRepository)); }
        }

        public CheckpointService CheckpointService
        {
            get { return _checkpointService ?? (_checkpointService = new CheckpointService(_cachedRepositoryFactory.GetCheckpointRepository(new SqlCheckpointRepository(CheckpointStorage)))); }
        }

        public EventService EventService
        {
            get { return _eventService ?? (_eventService = new EventService(_cachedRepositoryFactory.GetEventRepository(new SqlEventRepository(_db)))); }
        }

        public PlayerService PlayerService
        {
            get { return _playerService ?? (_playerService = new PlayerService(_cachedRepositoryFactory.GetPlayerRepository(new SqlPlayerRepository(_db)))); }
        }

        public UserService UserService
        {
            get { return _userService ?? (_userService = new UserService(_cachedRepositoryFactory.GetUserRepository(new SqlUserRepository(_db)))); }
        }

        public IRandomService RandomService
        {
            get { return _randomService ?? (_randomService = new RandomService()); }
        }

        public IMessageSender MessageSender
        {
            get { return _messageSender ?? (_messageSender = new MessageSender()); }
        }
        
        private ICheckpointRepository CheckpointRepository
        {
            get { return _checkpointRepository ?? (_checkpointRepository = _cachedRepositoryFactory.GetCheckpointRepository(new SqlCheckpointRepository(CheckpointStorage))); }
        }

        private ICheckpointStorage CheckpointStorage
        {
            get { return _checkpointStorage ?? (_checkpointStorage = new SqlServerCheckpointStorage()); }
        }

        private ICashgameStorage CashgameStorage
        {
            get { return _cashgameStorage ?? (_cashgameStorage = new SqlServerCashgameStorage()); }
        }
    }
}