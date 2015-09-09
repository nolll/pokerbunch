using Plumbing;
using Web.Common.Cache.Repositories;

namespace Web.Common.Cache
{
    public class Bootstrap
    {
        public UseCaseContainer UseCases { get; private set; }
        public CacheBuster CacheBuster { get; private set; }

        public Bootstrap()
        {
            var cacheContainer = new CacheContainer(new AspNetCacheProvider());
            var deps = new Dependencies(new CachedRepositoryFactory(cacheContainer));
            UseCases = new UseCaseContainer(deps);
            CacheBuster = new CacheBuster(deps, cacheContainer);
        }
    }
}