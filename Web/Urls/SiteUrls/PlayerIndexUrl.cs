using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class PlayerIndexUrl : SlugUrl
    {
        public PlayerIndexUrl(string slug)
            : base(WebRoutes.Player.List, slug)
        {
        }
    }
}