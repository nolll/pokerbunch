using Core.Repositories;
using Core.Services;
using Infrastructure.Environment;
using Infrastructure.Storage;
using Infrastructure.Storage.Cache;
using Infrastructure.Storage.Interfaces;
using Infrastructure.Storage.Repositories;
using Infrastructure.Web;

namespace Plumbing
{
    public class Dependencies
    {
        private ICacheProvider _cacheProvider;
        private ICacheContainer _cacheContainer;

        protected readonly ITimeProvider TimeProvider;
        protected readonly IRandomService RandomService;
        protected readonly IMessageSender MessageSender;
        protected readonly IBunchRepository BunchRepository;
        protected readonly IUserRepository UserRepository;
        protected readonly IPlayerRepository PlayerRepository;
        protected readonly ICashgameRepository CashgameRepository;
        protected readonly ICheckpointRepository CheckpointRepository;
        protected readonly IEventRepository EventRepository;
        protected readonly ICashgameService CashgameService;
        protected readonly IAuth Auth;

        protected Dependencies()
        {
            TimeProvider = new TimeProvider();
            RandomService = new RandomService();
            MessageSender = new MessageSender();
            
            var storage = new SqlStorage();

            var cacheBuster = new CacheBuster(CacheContainer, storage.Users, storage.Bunches, storage.Players, storage.Cashgames, storage.Checkpoints);
            
            BunchRepository = new SqlBunchRepository(storage.Bunches, CacheContainer, cacheBuster);
            UserRepository = new SqlUserRepository(storage.Users, CacheContainer, cacheBuster);
            PlayerRepository = new SqlPlayerRepository(storage.Players, CacheContainer, cacheBuster, UserRepository);
            CashgameRepository = new SqlCashgameRepository(storage.Cashgames, CacheContainer, storage.Checkpoints, TimeProvider, cacheBuster);
            CheckpointRepository = new SqlCheckpointRepository(storage.Checkpoints, cacheBuster);
            EventRepository = new SqlEventRepository(storage.Events, CacheContainer, cacheBuster);
            CashgameService = new CashgameService(PlayerRepository, CashgameRepository);
            Auth = new Auth(TimeProvider, UserRepository);
        }

        protected ICacheContainer CacheContainer
        {
            get { return _cacheContainer ?? (_cacheContainer = new CacheContainer(CacheProvider)); }
        }

        private ICacheProvider CacheProvider
        {
            get { return _cacheProvider ?? (_cacheProvider = new AspNetCacheProvider()); }
        }
    }

    public class SqlStorage
    {
        private IUserStorage _userStorage;
        private IBunchStorage _bunchStorage;
        private IPlayerStorage _playerStorage;
        private ICheckpointStorage _checkpointStorage;
        private ICashgameStorage _cashgameStorage;
        private IEventStorage _eventStorage;
        
        public IUserStorage Users
        {
            get { return _userStorage ?? (_userStorage = new SqlServerUserStorage()); }
        }

        public IBunchStorage Bunches
        {
            get { return _bunchStorage ?? (_bunchStorage = new SqlServerBunchStorage()); }
        }

        public IPlayerStorage Players
        {
            get { return _playerStorage ?? (_playerStorage = new SqlServerPlayerStorage()); }
        }

        public ICheckpointStorage Checkpoints
        {
            get { return _checkpointStorage ?? (_checkpointStorage = new SqlServerCheckpointStorage()); }
        }

        public ICashgameStorage Cashgames
        {
            get { return _cashgameStorage ?? (_cashgameStorage = new SqlServerCashgameStorage()); }
        }

        public IEventStorage Events
        {
            get { return _eventStorage ?? (_eventStorage = new SqlServerEventStorage()); }
        }
    }
}