using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class AddEventConfirmationUrl : SiteUrl
    {
        private readonly string _slug;

        public AddEventConfirmationUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Event.AddConfirmation, RouteParam.Slug(_slug));
    }
}