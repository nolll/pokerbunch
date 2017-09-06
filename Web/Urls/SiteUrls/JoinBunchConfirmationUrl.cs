using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class JoinBunchConfirmationUrl : SiteUrl
    {
        private readonly string _slug;

        public JoinBunchConfirmationUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Bunch.JoinConfirmation, RouteReplace.Slug(_slug));
    }
}