using Web.Common.Routes;

namespace Web.Common.Urls.SiteUrls
{
    public class EditLocationUrl : IdUrl
    {
        public EditLocationUrl(string locationId)
            : base(WebRoutes.Location.Edit, locationId)
        {
        }
    }
}