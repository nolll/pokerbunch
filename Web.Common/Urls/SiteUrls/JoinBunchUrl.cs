using Web.Common.Routes;

namespace Web.Common.Urls.SiteUrls
{
    public class JoinBunchUrl : SlugUrl
    {
        public JoinBunchUrl(string slug)
            : base(WebRoutes.BunchJoin, slug)
        {
        }
    }
}