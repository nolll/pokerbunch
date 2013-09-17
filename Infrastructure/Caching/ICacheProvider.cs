using System;

namespace Infrastructure.Caching
{
    public interface ICacheProvider
    {
        object Get(string key);
        void Put(string key, object obj, TimeSpan time);
        void Remove(string key);
    }
}