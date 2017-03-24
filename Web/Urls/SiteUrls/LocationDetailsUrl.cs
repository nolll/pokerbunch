using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class LocationDetailsUrl : SiteUrl
    {
        private readonly string _id;

        public LocationDetailsUrl(string id)
        {
            _id = id;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Location.Details, RouteParam.Id(_id));
    }
}