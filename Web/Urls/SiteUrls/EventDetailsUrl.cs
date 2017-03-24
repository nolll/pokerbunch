using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class EventDetailsUrl : SiteUrl
    {
        private readonly string _id;

        public EventDetailsUrl(string id)
        {
            _id = id;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Event.Details, RouteParam.Id(_id));
    }
}