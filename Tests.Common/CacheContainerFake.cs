using System;
using Infrastructure.Caching;

namespace Tests.Common
{
    public class CacheContainerFake : ICacheContainer
    {
        private string _fakedCacheKey;
        private object _fakedCacheValue;

        public CacheContainerFake()
        {
            _fakedCacheKey = null;
            _fakedCacheValue = null;
        }

        public bool TryGet<T>(string key, out T value) where T : class
        {
            value = _fakedCacheValue != null ? (T)_fakedCacheValue : default(T);
            return true;
        }

        public T Get<T>(string key) where T : class
        {
            return _fakedCacheValue != null ? (T)_fakedCacheValue : default(T);
        }

        public T GetCachedIfAvailable<T>(Func<T> fetchFromSourceExpression, TimeSpan cacheTime, string cacheKeyName,
                                         params object[] cacheKeyParams) where T : class
        {
            return _fakedCacheValue != null ? (T)_fakedCacheValue : default(T);
        }

        public string ConstructCacheKey(string typeName, params object[] procedureParameters)
        {
            return _fakedCacheKey;
        }

        public void Insert(string cacheKey, object objectToBeCached, TimeSpan cacheTime)
        {
            
        }

        public void SetFakedCacheKey(string fakedCacheKey)
        {
            _fakedCacheKey = fakedCacheKey;
        }

        public void SetFakedCacheValue<T>(T obj)
        {
            
        }

    }
}