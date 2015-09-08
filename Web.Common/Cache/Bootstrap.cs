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
            var deps = new Dependencies(new AspNetCacheProvider(), new CachedRepositoryFactory());
            UseCases = new UseCaseContainer(deps);
            CacheBuster = new CacheBuster(deps);
        }
    }
}