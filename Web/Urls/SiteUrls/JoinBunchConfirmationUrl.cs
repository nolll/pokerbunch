using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class JoinBunchConfirmationUrl : SlugUrl
    {
        public JoinBunchConfirmationUrl(string slug)
            : base(WebRoutes.Bunch.JoinConfirmation, slug)
        {
        }
    }
}