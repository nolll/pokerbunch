using System;
using Infrastructure.Caching;

namespace Tests.Common
{
    class CacheRepositoryFake : ICacheRepository
    {
        public bool TryGet<T>(string key, out T value) where T : class
        {
            value = null;
            return true;
        }

        public void Insert(string cacheKey, object objectToBeCached, TimeSpan cacheTime)
        {
            
        }

        public T GetCachedIfAvailable<T>(Func<T> fetchFromSourceExpression, TimeSpan cacheTime, string cacheKeyName,
                                         params object[] cacheKeyParams) where T : class
        {
            return fetchFromSourceExpression();
        }

        public T GetObjectStoreInCache<T>(Func<T> fetchFromSourceExpression, TimeSpan cacheTime, string cacheKeyName,
                                          params object[] cacheKeyParams) where T : class
        {
            return fetchFromSourceExpression();
        }

        public T GetCachedOrBackupedIfAvailable<T>(Func<T> fetchFromSourceExpression, TimeSpan cacheTime, string cacheKeyName,
                                                   params object[] cacheKeyParams) where T : class
        {
            return fetchFromSourceExpression();
        }
    }
}