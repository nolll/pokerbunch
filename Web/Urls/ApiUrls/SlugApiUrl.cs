using Web.Urls.SiteUrls;

namespace Web.Urls.ApiUrls
{
    public abstract class SlugApiUrl : ApiUrl
    {
        protected SlugApiUrl(string format, string slug)
            : base(RouteParams.ReplaceSlug(format, slug))
        {
        }
    }
}