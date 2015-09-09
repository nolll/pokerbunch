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

        public IUserRepository GetUserRepository(IUserRepository userRepository)
        {
            return new CachedUserRepository(userRepository, _cacheContainer);
        }

        public IBunchRepository GetBunchRepository(IBunchRepository bunchRepository)
        {
            return new CachedBunchRepository(bunchRepository, _cacheContainer);
        }
    }
}
