using System;
using System.Collections.Generic;
using Core.Classes;

namespace Infrastructure.Caching
{
    public interface ICacheContainer
    {
        string ConstructCacheKey(string typeName, params object[] procedureParameters);
        void Insert(string cacheKey, object objectToBeCached, TimeSpan cacheTime);
        void FakeInsert(string cacheKey, object objectToBeCached, TimeSpan cacheTime);
        T Get<T>(string key) where T : class;
        void Remove(string cacheKey);
        void FakeRemove(string cacheKey);
        T GetAndStore<T>(Func<T> fetchFromSourceExpression, TimeSpan cacheTime, string cacheKey) where T : class;
        IList<T> GetEachAndStore<T>(Func<IList<int>, IList<T>> fetchFromSourceExpression, TimeSpan cacheTime, IList<int> ids) where T : class, ICacheable;
    }
}