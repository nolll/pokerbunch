using Web.Common.Routes;

namespace Web.Common.Urls.SiteUrls
{
    public class EditAppUrl : IdUrl
    {
        public EditAppUrl(string appId)
            : base(WebRoutes.App.Edit, appId)
        {
        }
    }
}