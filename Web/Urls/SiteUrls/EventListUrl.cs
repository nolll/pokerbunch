using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class EventListUrl : SiteUrl
    {
        private readonly string _slug;

        public EventListUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Event.List, RouteParam.Slug(_slug));
    }
}