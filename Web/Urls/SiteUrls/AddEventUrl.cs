using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class AddEventUrl : SlugUrl
    {
        public AddEventUrl(string slug)
            : base(WebRoutes.Event.Add, slug)
        {
        }
    }
}