using System;

namespace Infrastructure.Caching
{
    public interface ICacheHandler
    {
        object Get(string key);
        void Put(string key, object obj, TimeSpan time);
    }
}