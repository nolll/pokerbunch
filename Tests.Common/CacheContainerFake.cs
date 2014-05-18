using System;
using System.Collections.Generic;
using Core.Entities;
using Infrastructure.Data.Cache;

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

        public string ConstructCacheKey(string typeName, params object[] procedureParameters)
        {
            return _fakedCacheKey;
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

        public T GetAndStore<T>(Func<T> sourceExpression, TimeSpan cacheTime, string cacheKey, bool allowCachedNullValue = false) where T : class
        {
            return sourceExpression();
        }

        public int? GetAndStore(Func<int?> sourceExpression, TimeSpan cacheTime, string cacheKey, bool allowCachedNullValue = false)
        {
            return sourceExpression();
        }

        public IList<T> GetEachAndStore<T>(Func<IList<int>, IList<T>> sourceExpression, TimeSpan cacheTime, IList<int> ids) where T : class, ICacheable
        {
            return sourceExpression(ids);
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