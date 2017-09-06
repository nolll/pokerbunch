using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class EditLocationUrl : SiteUrl
    {
        private readonly string _id;

        public EditLocationUrl(string id)
        {
            _id = id;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Location.Edit, RouteReplace.Id(_id));
    }
}