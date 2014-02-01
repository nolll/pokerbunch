using System;
using System.Web;

namespace Infrastructure.Data.Cache
{
    public class CacheProvider : ICacheProvider
    {
        public object Get(string key)
        {
            return HttpContext.Current.Cache.Get(key);
        }

        public void Put(string key, object obj, TimeSpan time)
        {
            HttpContext.Current.Cache.Insert(key, obj, null, System.Web.Caching.Cache.NoAbsoluteExpiration, time);
        }

        public void Remove(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }
    }
}
