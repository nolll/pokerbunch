using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class AddPlayerUrl : SlugUrl
    {
        public AddPlayerUrl(string slug)
            : base(WebRoutes.Player.Add, slug)
        {
        }
    }
}