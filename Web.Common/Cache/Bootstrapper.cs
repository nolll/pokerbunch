using Plumbing;

namespace Web.Common.Cache
{
    public class Bootstrapper
    {
        public UseCaseContainer UseCases { get; private set; }

        public Bootstrapper()
        {
            var cacheContainer = new CacheContainer(new AspNetCacheProvider());
            var deps = new Dependencies(cacheContainer);
            UseCases = new UseCaseContainer(deps);
        }
    }
}