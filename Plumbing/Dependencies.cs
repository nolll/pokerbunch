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

        public Dependencies(IRepositoryFactory cachedRepositoryFactory)
        {
            _cachedRepositoryFactory = cachedRepositoryFactory;
        }

        private AppService _appService;
        public AppService AppService
        {
            get { return _appService ?? (_appService = new AppService(AppRepository)); }
        }

        private BunchService _bunchService;
        public BunchService BunchService
        {
            get { return _bunchService ?? (_bunchService = new BunchService(BunchRepository)); }
        }

        private CashgameService _cashgameService;
        public CashgameService CashgameService
        {
            get { return _cashgameService ?? (_cashgameService = new CashgameService(CashgameRepository)); }
        }

        private CheckpointService _checkpointService;
        public CheckpointService CheckpointService
        {
            get { return _checkpointService ?? (_checkpointService = new CheckpointService(CheckpointRepository)); }
        }

        private EventService _eventService;
        public EventService EventService
        {
            get { return _eventService ?? (_eventService = new EventService(EventRepository1)); }
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
        
        private IBunchRepository _bunchRepository;
        public IBunchRepository BunchRepository
        {
            get { return _bunchRepository ?? (_bunchRepository = _cachedRepositoryFactory.GetBunchRepository(new SqlBunchRepository())); }
        }

        private IUserRepository _userRepository;
        public IUserRepository UserRepository
        {
            get { return _userRepository ?? (_userRepository = _cachedRepositoryFactory.GetUserRepository(new SqlUserRepository())); }
        }

        private IPlayerRepository _playerRepository;
        public IPlayerRepository PlayerRepository
        {
            get { return _playerRepository ?? (_playerRepository = _cachedRepositoryFactory.GetPlayerRepository(new SqlPlayerRepository(PlayerStorage, UserRepository))); }
        }

        private ICashgameRepository _cashgameRepository;
        public ICashgameRepository CashgameRepository
        {
            get { return _cashgameRepository ?? (_cashgameRepository = _cachedRepositoryFactory.GetCashgameRepository(new SqlCashgameRepository(CashgameStorage, CheckpointStorage))); }
        }

        private ICheckpointRepository _checkpointRepository;
        public ICheckpointRepository CheckpointRepository
        {
            get { return _checkpointRepository ?? (_checkpointRepository = _cachedRepositoryFactory.GetCheckpointRepository(new SqlCheckpointRepository(CheckpointStorage))); }
        }

        private IEventRepository _eventRepository;
        public IEventRepository EventRepository1
        {
            get { return _eventRepository ?? (_eventRepository = _cachedRepositoryFactory.GetEventRepository(new SqlEventRepository(EventStorage))); }
        }

        private IAppRepository _appRepository;
        private IAppRepository AppRepository
        {
            get { return _appRepository ?? (_appRepository = _cachedRepositoryFactory.GetAppRepository(new SqlAppRepository())); }
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

        private IEventStorage _eventStorage;
        private IEventStorage EventStorage
        {
            get { return _eventStorage ?? (_eventStorage = new SqlServerEventStorage()); }
        }
    }
}