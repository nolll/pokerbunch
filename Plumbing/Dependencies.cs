using Core.Repositories;
using Core.Services;
using Infrastructure.Environment;
using Infrastructure.Storage;
using Infrastructure.Storage.Cache;
using Infrastructure.Storage.Repositories;
using Infrastructure.Web;

namespace Plumbing
{
    public class Dependencies
    {
        protected readonly ITimeProvider TimeProvider;
        protected readonly IRandomService RandomService;
        protected readonly IMessageSender MessageSender;
        protected readonly ICacheContainer CacheContainer;
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
            var cacheProvider = new AspNetCacheProvider();
            TimeProvider = new TimeProvider();
            RandomService = new RandomService();
            MessageSender = new MessageSender();
            
            CacheContainer = new CacheContainer(cacheProvider);

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
    }

    public class SqlStorage
    {
        public readonly SqlServerUserStorage Users;
        public readonly SqlServerBunchStorage Bunches;
        public readonly SqlServerPlayerStorage Players;
        public readonly SqlServerCheckpointStorage Checkpoints;
        public readonly SqlServerCashgameStorage Cashgames;
        public readonly SqlServerEventStorage Events;

        public SqlStorage()
        {
            Users = new SqlServerUserStorage();
            Bunches = new SqlServerBunchStorage();
            Players = new SqlServerPlayerStorage();
            Checkpoints = new SqlServerCheckpointStorage();
            Cashgames = new SqlServerCashgameStorage();
            Events = new SqlServerEventStorage();
        }
    }
}