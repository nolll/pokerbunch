using Core.Services;

namespace Core.UseCases.ClearCache
{
    public static class ClearCacheInteractor
    {
        public static ClearCacheOutput Execute(ICacheContainer cacheContainer)
        {
            var objectCount = cacheContainer.ClearAll();

            return new ClearCacheOutput(objectCount);
        }
    }
}
