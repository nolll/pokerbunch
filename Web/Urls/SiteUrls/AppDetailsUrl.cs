using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class AppDetailsUrl : SiteUrl
    {
        private readonly string _id;

        public AppDetailsUrl(string id)
        {
            _id = id;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.App.Details, RouteReplace.Id(_id));
    }
}