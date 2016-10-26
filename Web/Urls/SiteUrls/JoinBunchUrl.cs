using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class JoinBunchUrl : SlugUrl
    {
        public JoinBunchUrl(string slug)
            : base(WebRoutes.Bunch.Join, slug)
        {
        }

        public JoinBunchUrl(string slug, string code)
            : base(WebRoutes.Bunch.JoinWithCode.Replace("{code}", code), slug)
        {
        }
    }
}