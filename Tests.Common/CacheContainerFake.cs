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

        public T Get<T>(string key) where T : class
        {
            return _fakedCacheValue != null ? (T)_fakedCacheValue : default(T);
        }

        public T GetEmpty<T>(string key) where T : class
        {
            return null;
        }

        public string ConstructCacheKey(string typeName, params object[] procedureParameters)
        {
            return _fakedCacheKey;
        }

        public void Insert(string cacheKey, object objectToBeCached, TimeSpan cacheTime)
        {
        }

        public void FakeInsert(string cacheKey, object objectToBeCached, TimeSpan cacheTime)
        {
        }

        public void Remove(string cacheKey)
        {
        }

        public void FakeRemove(string cacheKey)
        {
        }

        public T GetAndStore<T>(Func<T> fetchFromSourceExpression, TimeSpan cacheTime, string cacheKey) where T : class
        {
            return Get<T>(cacheKey);
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