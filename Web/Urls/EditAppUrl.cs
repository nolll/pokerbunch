using Web.Common.Routes;

namespace Web.Urls
{
    public class EditAppUrl : IdUrl
    {
        public EditAppUrl(int appId)
            : base(WebRoutes.AppEdit, appId)
        {
        }
    }
}