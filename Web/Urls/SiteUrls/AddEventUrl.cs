using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class AddEventUrl : SiteUrl
    {
        private readonly string _slug;

        public AddEventUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Event.Add, RouteReplace.Slug(_slug));
    }
}