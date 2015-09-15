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

        public IAppRepository CreateAppRepository(IAppRepository appRepository)
        {
            return new CachedAppRepository(appRepository, _cacheContainer);
        }

        public IBunchRepository CreateBunchRepository(IBunchRepository bunchRepository)
        {
            return new CachedBunchRepository(bunchRepository, _cacheContainer);
        }

        public ICashgameRepository CreateCashgameRepository(ICashgameRepository cashgameRepository)
        {
            return new CachedCashgameRepository(cashgameRepository, _cacheContainer);
        }
        
        public IEventRepository CreateEventRepository(IEventRepository eventRepository)
        {
            return new CachedEventRepository(eventRepository, _cacheContainer);
        }

        public IPlayerRepository CreatePlayerRepository(IPlayerRepository playerRepository)
        {
            return new CachedPlayerRepository(playerRepository, _cacheContainer);
        }

        public IUserRepository CreateUserRepository(IUserRepository userRepository)
        {
            return new CachedUserRepository(userRepository, _cacheContainer);
        }
    }
}
