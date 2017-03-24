using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class AddLocationConfirmationUrl : SiteUrl
    {
        private readonly string _slug;

        public AddLocationConfirmationUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Location.AddConfirmation, RouteParam.Slug(_slug));
    }
}