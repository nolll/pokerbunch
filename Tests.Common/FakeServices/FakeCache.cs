using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Services;

namespace Tests.Common.FakeServices
{
    public class FakeCache : ICacheContainer
    {
        public void Remove(string cacheKey)
        {
            throw new NotImplementedException();
        }

        public int ClearAll()
        {
            return 1;
        }

        public T GetAndStore<T>(Func<T> sourceExpression, TimeSpan cacheTime, string cacheKey, bool allowCachedNullValue = false) where T : class
        {
            throw new NotImplementedException();
        }

        public int? GetAndStore(Func<int?> sourceExpression, TimeSpan cacheTime, string cacheKey, bool allowCachedNullValue = false)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAndStore<T>(Func<IList<int>, IList<T>> sourceExpression, TimeSpan cacheTime, IList<int> ids) where T : class, IEntity
        {
            throw new NotImplementedException();
        }
    }
}