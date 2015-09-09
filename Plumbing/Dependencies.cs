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

        private UserService _userService;
        public UserService UserService
        {
            get { return _userService ?? (_userService = new UserService(UserRepository)); }
        }

        private BunchService _bunchService;
        public BunchService BunchService
        {
            get { return _bunchService ?? (_bunchService = new BunchService(BunchRepository)); }
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
            get { return _bunchRepository ?? (_bunchRepository = _cachedRepositoryFactory.GetBunchRepository(new SqlBunchRepository(BunchStorage))); }
        }

        private IUserRepository _userRepository;
        public IUserRepository UserRepository
        {
            get { return _userRepository ?? (_userRepository = _cachedRepositoryFactory.GetUserRepository(new SqlUserRepository())); }
        }

        private IPlayerRepository _playerRepository;
        public IPlayerRepository PlayerRepository
        {
            get { return _playerRepository ?? (_playerRepository = new SqlPlayerRepository(PlayerStorage, UserRepository)); }
        }

        private ICashgameRepository _cashgameRepository;
        public ICashgameRepository CashgameRepository
        {
            get { return _cashgameRepository ?? (_cashgameRepository = new SqlCashgameRepository(CashgameStorage, CheckpointStorage)); }
        }

        private ICheckpointRepository _checkpointRepository;
        public ICheckpointRepository CheckpointRepository
        {
            get { return _checkpointRepository ?? (_checkpointRepository = new SqlCheckpointRepository(CheckpointStorage)); }
        }

        private IEventRepository _eventRepository;
        public IEventRepository EventRepository
        {
            get { return _eventRepository ?? (_eventRepository = new SqlEventRepository(EventStorage)); }
        }

        private IAppRepository _appRepository;
        public IAppRepository AppRepository
        {
            get { return _appRepository ?? (_appRepository = new SqlAppRepository()); }
        }

        private IBunchStorage _bunchStorage;
        private IBunchStorage BunchStorage
        {
            get { return _bunchStorage ?? (_bunchStorage = new SqlServerBunchStorage()); }
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