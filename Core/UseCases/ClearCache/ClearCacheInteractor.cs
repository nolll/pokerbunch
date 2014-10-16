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

    public class ClearCacheOutput
    {
        public int ObjectCount { get; private set; }

        public ClearCacheOutput(int objectCount)
        {
            ObjectCount = objectCount;
        }
    }
}
