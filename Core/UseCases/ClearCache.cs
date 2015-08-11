using Core.Services;

namespace Core.UseCases
{
    public class ClearCache
    {
        private readonly ICacheContainer _cacheContainer;

        public ClearCache(ICacheContainer cacheContainer)
        {
            _cacheContainer = cacheContainer;
        }

        public Result Execute()
        {
            var objectCount = _cacheContainer.ClearAll();

            return new Result(objectCount);
        }

        public class Result
        {
            public int DeleteCount { get; private set; }

            public Result(int deleteCount)
            {
                DeleteCount = deleteCount;
            }
        }
    }
}
