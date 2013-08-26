using System;
using Infrastructure.Caching;

namespace Tests.Common
{
    class CacheContainerFake : ICacheContainer
    {
        public bool TryGet<T>(string key, out T value) where T : class
        {
            value = null;
            return true;
        }

        public T GetCachedIfAvailable<T>(Func<T> fetchFromSourceExpression, TimeSpan cacheTime, string cacheKeyName,
                                         params object[] cacheKeyParams) where T : class
        {
            return fetchFromSourceExpression();
        }

        public string ConstructCacheKey(string typeName, params object[] procedureParameters)
        {
            return null;
        }

        public void Insert(string cacheKey, object objectToBeCached, TimeSpan cacheTime)
        {
            
        }
    }
}