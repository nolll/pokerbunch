using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
    public interface ICacheContainer
    {
        void Remove(string cacheKey);
        int ClearAll();
        T GetAndStore<T>(Func<T> sourceExpression, TimeSpan cacheTime, string cacheKey, bool allowCachedNullValue = false) where T : class;
        int? GetAndStore(Func<int?> sourceExpression, TimeSpan cacheTime, string cacheKey, bool allowCachedNullValue = false);
        IList<T> GetEachAndStore<T>(Func<IList<int>, IList<T>> sourceExpression, TimeSpan cacheTime, IList<int> ids) where T : class, IEntity;
    }
}