using Core.Services;

namespace Core.UseCases.ClearCache
{
    public class ClearCacheInteractor
    {
        private readonly ICacheContainer _cacheContainer;

        public ClearCacheInteractor(ICacheContainer cacheContainer)
        {
            _cacheContainer = cacheContainer;
        }

        public ClearCacheOutput Execute()
        {
            var objectCount = _cacheContainer.ClearAll();

            return new ClearCacheOutput(objectCount);
        }
    }
}
