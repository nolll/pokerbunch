using Core.Repositories;
using Core.Services;
using Core.Services.Interfaces;
using Infrastructure.Data.Cache;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Mappers;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.SqlServer;
using Infrastructure.System;
using Infrastructure.Web;

namespace Plumbing
{
    public class Dependencies
    {
        protected static readonly ITimeProvider TimeProvider = new TimeProvider();
        protected static readonly IRandomService RandomService = new RandomService();
        protected static readonly IWebContext WebContext = new WebContext();
        private static readonly IStorageProvider StorageProvider = new SqlServerStorageProvider();
        private static readonly ICacheProvider CacheProvider = new CacheProvider();
        private static readonly ICacheContainer CacheContainer = new CacheContainer(CacheProvider);
        private static readonly ICacheBuster CacheBuster = new CacheBuster(CacheContainer);
        private static readonly IBunchStorage BunchStorage = new SqlServerBunchStorage(StorageProvider);
        protected static readonly IBunchRepository BunchRepository = new BunchRepository(BunchStorage, CacheContainer, CacheBuster);
        private static readonly IUserStorage UserStorage = new SqlServerUserStorage(StorageProvider);
        protected static readonly IUserRepository UserRepository = new UserRepository(UserStorage, CacheContainer, CacheBuster);
        private static readonly IPlayerStorage PlayerStorage = new SqlServerPlayerStorage(StorageProvider);
        private static readonly IPlayerDataMapper PlayerDataMapper = new PlayerDataMapper(UserRepository);
        protected static readonly IPlayerRepository PlayerRepository = new PlayerRepository(PlayerStorage, PlayerDataMapper, CacheContainer, CacheBuster);
        private static readonly IRawCashgameFactory RawCashgameFactory = new RawCashgameFactory(TimeProvider);
        private static readonly ICheckpointStorage CheckpointStorage = new SqlServerCheckpointStorage(StorageProvider, TimeProvider);
        private static readonly ICashgameStorage CashgameStorage = new SqlServerCashgameStorage(StorageProvider, RawCashgameFactory);
        protected static readonly ICashgameRepository CashgameRepository = new CashgameRepository(CashgameStorage, RawCashgameFactory, CacheContainer, CheckpointStorage, CacheBuster);
        protected static readonly ICashgameService CashgameService = new CashgameService(PlayerRepository, CashgameRepository);
        protected static readonly ICheckpointRepository CheckpointRepository = new CheckpointRepository(CheckpointStorage, CacheBuster);
        protected static readonly IAuth Auth = new Auth(TimeProvider, UserRepository);
        protected static readonly IMessageSender MessageSender = new MessageSender();
    }
}