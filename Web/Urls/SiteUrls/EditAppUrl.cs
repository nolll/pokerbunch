using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class EditAppUrl : IdUrl
    {
        public EditAppUrl(string appId)
            : base(WebRoutes.App.Edit, appId)
        {
        }
    }
}