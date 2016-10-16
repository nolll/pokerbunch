using Plumbing;

namespace Web.Common.Cache
{
    public class Bootstrapper
    {
        public UseCaseContainer UseCases { get; private set; }

        public Bootstrapper(string connectionString, string apiUrl, string apiKey, string apiToken)
        {
            var cacheContainer = new CacheContainer(new AspNetCacheProvider());
            var deps = new Dependencies(cacheContainer, connectionString, apiUrl, apiKey, apiToken);
            UseCases = new UseCaseContainer(deps);
        }
    }
}