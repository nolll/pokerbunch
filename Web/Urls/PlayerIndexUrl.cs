using Web.Common.Routes;

namespace Web.Urls
{
    public class PlayerIndexUrl : SlugUrl
    {
        public PlayerIndexUrl(string slug)
            : base(WebRoutes.PlayerList, slug)
        {
        }
    }
}