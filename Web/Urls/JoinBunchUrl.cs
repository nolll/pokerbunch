using Web.Common.Routes;

namespace Web.Urls
{
    public class JoinBunchUrl : SlugUrl
    {
        public JoinBunchUrl(string slug)
            : base(WebRoutes.BunchJoin, slug)
        {
        }
    }
}