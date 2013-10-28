using System;

namespace Infrastructure.Caching
{
    public interface ICacheContainer
    {
        string ConstructCacheKey(string typeName, params object[] procedureParameters);
        void Insert(string cacheKey, object objectToBeCached, TimeSpan cacheTime);
        T Get<T>(string key) where T : class;
        void Remove(string cacheKey);
    }
}