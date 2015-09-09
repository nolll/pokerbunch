using Core.Repositories;
using Core.Services;

namespace Web.Common.Cache.Repositories
{
    public class CachedRepositoryFactory : IRepositoryFactory
    {
        private readonly ICacheContainer _cacheContainer;

        public CachedRepositoryFactory(ICacheContainer cacheContainer)
        {
            _cacheContainer = cacheContainer;
        }

        public IAppRepository GetAppRepository(IAppRepository appRepository)
        {
            return new CachedAppRepository(appRepository, _cacheContainer);
        }

        public IBunchRepository GetBunchRepository(IBunchRepository bunchRepository)
        {
            return new CachedBunchRepository(bunchRepository, _cacheContainer);
        }

        public ICashgameRepository GetCashgameRepository(ICashgameRepository cashgameRepository)
        {
            return new CachedCashgameRepository(cashgameRepository, _cacheContainer);
        }

        public ICheckpointRepository GetCheckpointRepository(ICheckpointRepository checkpointRepository)
        {
            return new CachedCheckpointRepository(checkpointRepository, _cacheContainer);
        }

        public IEventRepository GetEventRepository(IEventRepository eventRepository)
        {
            return new CachedEventRepository(eventRepository, _cacheContainer);
        }

        public IPlayerRepository GetPlayerRepository(IPlayerRepository playerRepository)
        {
            return new CachedPlayerRepository(playerRepository, _cacheContainer);
        }

        public IUserRepository GetUserRepository(IUserRepository userRepository)
        {
            return new CachedUserRepository(userRepository, _cacheContainer);
        }
    }
}
