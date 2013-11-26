using System;

namespace Infrastructure.Caching
{
    public class CacheContainer : ICacheContainer
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly ICacheKeyProvider _cacheKeyProvider;

        private readonly CacheableNullValue _nullValue = new CacheableNullValue();

        public CacheContainer(ICacheProvider cacheProvider, ICacheKeyProvider cacheKeyProvider)
        {
            _cacheProvider = cacheProvider;
            _cacheKeyProvider = cacheKeyProvider;
        }

        public T Get<T>(string cacheKey) where T : class
        {
            T cachedObject;
            var foundInCache = TryGet(cacheKey, out cachedObject);

            if (!foundInCache)
            {
                return default(T);
            }

            return cachedObject;
        }

        public T GetAndStore<T>(Func<T> fetchFromSourceExpression, TimeSpan cacheTime, string cacheKey) where T : class
        {
            T cachedObject;
            var foundInCache = TryGet(cacheKey, out cachedObject);

            if (!foundInCache)
            {
                cachedObject = fetchFromSourceExpression();

                Insert(cacheKey, cachedObject, cacheTime);
            }

            return cachedObject;
        }

        private bool TryGet<T>(string key, out T value) where T : class
        {
            var o = _cacheProvider.Get(key);

            if (o is CacheableNullValue)
            {
                // A fake null value was found in the cache
                value = default(T);
                return true;
            }

            if (o == null)
            {
                // A real null was found, this means that 'nothing is cached for this key
                value = default(T);
                return false;
            }

            value = (T)o;
            return true;
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