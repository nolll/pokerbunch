using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class AddLocationUrl : SlugUrl
    {
        public AddLocationUrl(string slug)
            : base(WebRoutes.Location.Add, slug)
        {
        }
    }
}