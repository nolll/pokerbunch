using Core.Repositories;
using Core.Services;
using Infrastructure.Storage;
using Infrastructure.Storage.Cache;
using Infrastructure.Storage.Interfaces;
using Infrastructure.Storage.Repositories;
using Infrastructure.Web;

namespace Plumbing
{
    public class Dependencies
    {
        private IRandomService _randomService;
        protected IRandomService RandomService
        {
            get { return _randomService ?? (_randomService = new RandomService()); }
        }

        private IMessageSender _messageSender;
        protected IMessageSender MessageSender
        {
            get { return _messageSender ?? (_messageSender = new MessageSender()); }
        }

        private ICacheContainer _cacheContainer;
        protected ICacheContainer CacheContainer
        {
            get { return _cacheContainer ?? (_cacheContainer = new CacheContainer(CacheProvider)); }
        }

        private ICacheProvider _cacheProvider;
        private ICacheProvider CacheProvider
        {
            get { return _cacheProvider ?? (_cacheProvider = new AspNetCacheProvider()); }
        }

        private ICacheBuster _cacheBuster;
        private ICacheBuster CacheBuster
        {
            get { return _cacheBuster ?? (_cacheBuster = new CacheBuster(CacheContainer, UserStorage, BunchStorage, PlayerStorage, CashgameStorage, CheckpointStorage)); }
        }

        private IBunchRepository _bunchRepository;
        protected IBunchRepository BunchRepository
        {
            get { return _bunchRepository ?? (_bunchRepository = new SqlBunchRepository(BunchStorage, CacheContainer, CacheBuster)); }
        }

        private IUserRepository _userRepository;
        protected IUserRepository UserRepository
        {
            get { return _userRepository ?? (_userRepository = new SqlUserRepository(UserStorage, CacheContainer, CacheBuster)); }
        }

        private IPlayerRepository _playerRepository;
        protected IPlayerRepository PlayerRepository
        {
            get { return _playerRepository ?? (_playerRepository = new SqlPlayerRepository(PlayerStorage, CacheContainer, CacheBuster, UserRepository)); }
        }

        private ICashgameRepository _cashgameRepository;
        protected ICashgameRepository CashgameRepository
        {
            get { return _cashgameRepository ?? (_cashgameRepository = new SqlCashgameRepository(CashgameStorage, CacheContainer, CheckpointStorage, CacheBuster)); }
        }

        private ICheckpointRepository _checkpointRepository;
        protected ICheckpointRepository CheckpointRepository
        {
            get { return _checkpointRepository ?? (_checkpointRepository = new SqlCheckpointRepository(CheckpointStorage, CacheBuster)); }
        }

        private IEventRepository _eventRepository;
        protected IEventRepository EventRepository
        {
            get { return _eventRepository ?? (_eventRepository = new SqlEventRepository(EventStorage, CacheContainer, CacheBuster)); }
        }

        private IUserStorage _userStorage;
        private IUserStorage UserStorage
        {
            get { return _userStorage ?? (_userStorage = new SqlServerUserStorage()); }
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