using Core.Repositories;
using Core.Services;

namespace Web.Common.Cache.Repositories
{
    public class CachedRepositoryFactory : IRepositoryFactory
    {
        private readonly ICacheContainer _cacheContainer = new CacheContainer(new AspNetCacheProvider());

        public IUserRepository GetUserRepository(IUserRepository userRepository)
        {
            return new CachedUserRepository(userRepository, _cacheContainer);
        }
    }
}
