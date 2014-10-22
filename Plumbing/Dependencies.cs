using Core.Repositories;
using Core.Services;
using Infrastructure.Cache;
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
        protected static readonly ITimeProvider TimeProvider = new TimeProvider();
        protected static readonly IRandomService RandomService = new RandomService();
        protected static readonly IMessageSender MessageSender = new MessageSender();
        protected static readonly IWebContext WebContext = new WebContext();

        private static readonly ICacheProvider CacheProvider = new AspNetCacheProvider();
        protected static readonly ICacheContainer CacheContainer = new CacheContainer(CacheProvider);
        private static readonly ICacheBuster CacheBuster = new CacheBuster(CacheContainer);
        private static readonly IRawCashgameFactory RawCashgameFactory = new RawCashgameFactory(TimeProvider);
        
        private static readonly IBunchStorage BunchStorage = new SqlServerBunchStorage();
        private static readonly IUserStorage UserStorage = new SqlServerUserStorage();
        private static readonly IPlayerStorage PlayerStorage = new SqlServerPlayerStorage();
        private static readonly ICheckpointStorage CheckpointStorage = new SqlServerCheckpointStorage();
        private static readonly ICashgameStorage CashgameStorage = new SqlServerCashgameStorage(RawCashgameFactory);
        private static readonly IEventStorage EventStorage = new SqlServerEventStorage();
        
        protected static readonly IBunchRepository BunchRepository = new SqlBunchRepository(BunchStorage, CacheContainer, CacheBuster);
        protected static readonly IUserRepository UserRepository = new SqlUserRepository(UserStorage, CacheContainer, CacheBuster);
        protected static readonly IPlayerRepository PlayerRepository = new SqlPlayerRepository(PlayerStorage, CacheContainer, CacheBuster, UserRepository);
        protected static readonly ICashgameRepository CashgameRepository = new SqlCashgameRepository(CashgameStorage, RawCashgameFactory, CacheContainer, CheckpointStorage, CacheBuster);
        protected static readonly ICheckpointRepository CheckpointRepository = new SqlCheckpointRepository(CheckpointStorage, CacheBuster);
        protected static readonly IEventRepository EventRepository = new SqlEventRepository(EventStorage, CacheContainer, CacheBuster);
        
        protected static readonly ICashgameService CashgameService = new CashgameService(PlayerRepository, CashgameRepository);
        protected static readonly IAuth Auth = new Auth(TimeProvider, UserRepository);
    }
}