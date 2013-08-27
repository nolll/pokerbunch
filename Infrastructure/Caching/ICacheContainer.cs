using System;

namespace Infrastructure.Caching
{
    public interface ICacheContainer
    {
        bool TryGet<T>(string key, out T value) where T : class;
        //void Insert(string cacheKey, object objectToBeCached, TimeSpan cacheTime);
        T GetCachedIfAvailable<T>(Func<T> fetchFromSourceExpression, TimeSpan cacheTime, string cacheKeyName, params object[] cacheKeyParams) where T : class;
        //T GetObjectStoreInCache<T>(Func<T> fetchFromSourceExpression, TimeSpan cacheTime, string cacheKeyName, params object[] cacheKeyParams) where T : class;
        //T GetCachedOrBackupedIfAvailable<T>(Func<T> fetchFromSourceExpression, TimeSpan cacheTime, string cacheKeyName, params object[] cacheKeyParams) where T : class;
        string ConstructCacheKey(string typeName, params object[] procedureParameters);
        void Insert(string cacheKey, object objectToBeCached, TimeSpan cacheTime);
        T Get<T>(string key) where T : class;
    }
}