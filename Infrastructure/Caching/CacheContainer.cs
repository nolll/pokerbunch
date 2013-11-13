using System;
using System.Collections.Concurrent;
using System.Text;

namespace Infrastructure.Caching
{
    public class CacheContainer : ICacheContainer
    {
        private static readonly ConcurrentDictionary<string, object> CurrentRequests = new ConcurrentDictionary<string, object>();

        private readonly ICacheProvider _cacheProvider;
        private readonly ICacheKeyProvider _cacheKeyProvider;

        private readonly CacheableNullValue _nullValue = new CacheableNullValue();

        public CacheContainer(ICacheProvider cacheProvider, ICacheKeyProvider cacheKeyProvider)
        {
            _cacheProvider = cacheProvider;
            _cacheKeyProvider = cacheKeyProvider;
        }

        public T Get<T>(string key) where T : class
        {
            var o = _cacheProvider.Get(key);

            if (o is CacheableNullValue)
            {
                // A fake null value was found in the cache
                return default(T);
            }

            if (o == null)
            {
                // A real null was found, this means that nothing is cached for this key
                return default(T);
            }

            return (T)o;
        }

        public void Insert(string cacheKey, object objectToBeCached, TimeSpan cacheTime)
        {
            _cacheProvider.Put(cacheKey, objectToBeCached ?? _nullValue, cacheTime);
        }

        public void FakeInsert(string cacheKey, object objectToBeCached, TimeSpan cacheTime)
        {
        }

        public void Remove(string cacheKey)
        {
            _cacheProvider.Remove(cacheKey);
        }

        public void FakeRemove(string cacheKey)
        {
            _cacheProvider.Remove(cacheKey);
        }

        public string ConstructCacheKey(string typeName, params object[] procedureParameters)
        {
            return _cacheKeyProvider.ConstructCacheKey(typeName, procedureParameters);
        }

        private class CacheableNullValue
        {
        }
    }
}