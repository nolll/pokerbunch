using Core.Urls;

namespace Web.Urls
{
    public class ClearCacheUrl : Url
    {
        public ClearCacheUrl()
            : base("-/admin/clearcache")
        {
        }
    }
}