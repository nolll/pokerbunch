using Core.Repositories;
using Core.Services;
using Infrastructure.Cache;
using Infrastructure.RavenDb.Repositories;
using Infrastructure.SqlServer;
using Infrastructure.SqlServer.Factories;
using Infrastructure.SqlServer.Factories.Interfaces;
using Infrastructure.SqlServer.Interfaces;
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
        protected readonly IWebContext WebContext;
        protected readonly ICacheContainer CacheContainer;
        protected readonly IRavenUserRepository RavenUserRepository;
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
            var cacheBuster = new CacheBuster(CacheContainer);
            var rawCashgameFactory = new RawCashgameFactory(TimeProvider);
            var bunchStorage = new SqlServerBunchStorage();
            var playerStorage = new SqlServerPlayerStorage();
            var checkpointStorage = new SqlServerCheckpointStorage();
            var cashgameStorage = new SqlServerCashgameStorage(rawCashgameFactory);
            var eventStorage = new SqlServerEventStorage();

            TimeProvider = new TimeProvider();
            RandomService = new RandomService();
            MessageSender = new MessageSender();
            WebContext = new WebContext();
            CacheContainer = new CacheContainer(cacheProvider);
            RavenUserRepository = new TempRavenUserRepository();
            BunchRepository = new SqlBunchRepository(bunchStorage, CacheContainer, cacheBuster);
            UserRepository = new RavenUserRepository();
            PlayerRepository = new SqlPlayerRepository(playerStorage, CacheContainer, cacheBuster, UserRepository);
            CashgameRepository = new SqlCashgameRepository(cashgameStorage, rawCashgameFactory, CacheContainer, checkpointStorage, cacheBuster);
            CheckpointRepository = new SqlCheckpointRepository(checkpointStorage, cacheBuster);
            EventRepository = new SqlEventRepository(eventStorage, CacheContainer, cacheBuster);
            CashgameService = new CashgameService(PlayerRepository, CashgameRepository);
            Auth = new Auth(TimeProvider, UserRepository);
        }
    }
}