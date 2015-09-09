using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.Entities;
using Core.Services;

namespace Web.Common.Cache
{
    public class CacheContainer : ICacheContainer
    {
        private readonly ICacheProvider _cacheProvider;

        private readonly CacheableNullValue _nullValue = new CacheableNullValue();

        public CacheContainer(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public int ClearAll()
        {
            return _cacheProvider.ClearAll();
        }

        public T GetAndStore<T>(Func<T> sourceExpression, TimeSpan cacheTime, string cacheKey, bool allowCachedNullValue) where T : class
        {
            T cachedObject;
            var foundInCache = TryGet(cacheKey, out cachedObject);

            if (foundInCache)
            {
                return cachedObject;
            }
            cachedObject = sourceExpression();
            if (cachedObject != null)
            {
                Insert(cacheKey, cachedObject, cacheTime);
            }
            else if (allowCachedNullValue)
            {
                Insert(cacheKey, new CacheableNullValue(), cacheTime);
            }
            return cachedObject;
        }

        public int? GetAndStore(Func<int?> sourceExpression, TimeSpan cacheTime, string cacheKey, bool allowCachedNullValue)
        {
            string cachedString;
            var foundInCache = TryGet(cacheKey, out cachedString);

            if (foundInCache)
            {
                if (cachedString == null)
                {
                    return null;
                }
                return int.Parse(cachedString);
            }
            var cachedInt = sourceExpression();
            if (cachedInt.HasValue)
            {
                Insert(cacheKey, cachedInt.Value.ToString(CultureInfo.InvariantCulture), cacheTime);
            }
            else if (allowCachedNullValue)
            {
                Insert(cacheKey, new CacheableNullValue(), cacheTime);
            }
            return cachedInt;
        }

        public IList<T> GetAndStore<T>(Func<IList<int>, IList<T>> sourceExpression, TimeSpan cacheTime, IList<int> ids) where T : class, IEntity
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
                var sourceItems = sourceExpression(notInCache);
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

        private static IList<T> OrderItemsByIdList<T>(IEnumerable<int> ids, IEnumerable<T> list) where T : class, IEntity
        {
            var result = ids.Select(id => list.FirstOrDefault(i => i.Id == id)).ToList();
            return result.Where(r => r != null).ToList();
        }

        private bool TryGet<T>(string key, out T value) where T : class
        {
            // Uncomment this row to temporarily disable cache in development
            //value = null; return false;

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

        private void Insert(string cacheKey, object objectToBeCached, TimeSpan cacheTime)
        {
            _cacheProvider.Put(cacheKey, objectToBeCached ?? _nullValue, cacheTime);
        }

        public void Remove(string cacheKey)
        {
            _cacheProvider.Remove(cacheKey);
        }

        private string ConstructCacheKey(string typeName, params object[] procedureParameters)
        {
            return CacheKeyProvider.ConstructCacheKey(typeName, procedureParameters);
        }

        private class CacheableNullValue
        {
        }
    }
}