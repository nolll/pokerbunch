using Web.Routes;

namespace Web.Urls.ApiUrls
{
    public class ApiBuyinUrl : SlugApiUrl
    {
        public ApiBuyinUrl(string slug)
            : base(ApiRoutes.Buyin, slug)
        {
        }
    }
}