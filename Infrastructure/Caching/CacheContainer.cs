using System;
using System.Collections.Generic;
using System.Linq;
using Core.Classes;

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

        public IList<T> GetEachAndStore<T>(Func<IList<int>, IList<T>> fetchFromSourceExpression, TimeSpan cacheTime, IList<int> ids) where T : class, ICacheable
        {
            var list = new List<T>();
            var notInCache = new List<int>();
            var cacheKeyName = typeof (T).ToString();
            foreach (var id in ids)
            {
                T cachedObject;
                var cacheKey = ConstructCacheKey(cacheKeyName, id);
                var foundInCache = TryGet(cacheKey, out cachedObject);
                if (foundInCache)
                    list.Add(cachedObject);
                else
                {
                    notInCache.Add(id);
                }
            }
            if (notInCache.Any())
            {
                var sourceItems = fetchFromSourceExpression(notInCache);
                foreach (var sourceItem in sourceItems)
                {
                    if (sourceItem != null) //Om något id inte har hämtats så stoppar vi inte in det i vårt resultat eller i cachen.
                    {
                        var cacheKey = ConstructCacheKey(cacheKeyName, sourceItem.Id);
                        Insert(cacheKey, sourceItem, cacheTime);
                    }
                }

                list = list.Concat(sourceItems.Where(o => o != null)).ToList();
                return OrderItemsByIdList(ids, list);
            }
            return list;
        }

        private static IList<T> OrderItemsByIdList<T>(IList<int> ids, IList<T> list) where T : class, ICacheable
        {
            var result = ids.Select(id => list.FirstOrDefault(i => i.Id == id)).ToList();
            return result.Where(r => r != null).ToList();
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