using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
    public interface ICacheContainer
    {
        void Remove(string cacheKey);
        void Remove<T>(string id);
        int ClearAll();
        T GetAndStore<T>(Func<string, T> sourceExpression, string id, TimeSpan cacheTime) where T : class, IEntity;
        IList<T> GetAndStore<T>(Func<IList<string>, IList<T>> sourceExpression, IList<string> ids, TimeSpan cacheTime) where T : class, IEntity;
    }
}