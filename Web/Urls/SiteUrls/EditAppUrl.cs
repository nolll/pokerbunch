using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class EditAppUrl : SiteUrl
    {
        private readonly string _id;

        public EditAppUrl(string id)
        {
            _id = id;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.App.Edit, RouteParam.Id(_id));
    }
}