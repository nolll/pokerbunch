using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class AddPlayerConfirmationUrl : SiteUrl
    {
        private readonly string _slug;

        public AddPlayerConfirmationUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Player.AddConfirmation, RouteReplace.Slug(_slug))
        ;
    }
}