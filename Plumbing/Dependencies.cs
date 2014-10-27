using Core.Repositories;
using Core.Services;
using Infrastructure.Cache;
using Infrastructure.SqlServer;
using Infrastructure.SqlServer.Repositories;
using Infrastructure.System;
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
            
            var userStorage = new SqlServerUserStorage();
            var bunchStorage = new SqlServerBunchStorage();
            var playerStorage = new SqlServerPlayerStorage();
            var checkpointStorage = new SqlServerCheckpointStorage();
            var cashgameStorage = new SqlServerCashgameStorage();
            var eventStorage = new SqlServerEventStorage();

            var cacheBuster = new CacheBuster(CacheContainer, userStorage, bunchStorage, playerStorage, cashgameStorage, checkpointStorage);
            
            BunchRepository = new SqlBunchRepository(bunchStorage, CacheContainer, cacheBuster);
            UserRepository = new SqlUserRepository(userStorage, CacheContainer, cacheBuster);
            PlayerRepository = new SqlPlayerRepository(playerStorage, CacheContainer, cacheBuster, UserRepository);
            CashgameRepository = new SqlCashgameRepository(cashgameStorage, CacheContainer, checkpointStorage, TimeProvider, cacheBuster);
            CheckpointRepository = new SqlCheckpointRepository(checkpointStorage, cacheBuster);
            EventRepository = new SqlEventRepository(eventStorage, CacheContainer, cacheBuster);
            CashgameService = new CashgameService(PlayerRepository, CashgameRepository);
            Auth = new Auth(TimeProvider, UserRepository);
        }
    }
}