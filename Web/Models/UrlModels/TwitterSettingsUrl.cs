using Web.Routing;

namespace Web.Models.UrlModels
{
    public class TwitterSettingsUrl : Url
    {
        public TwitterSettingsUrl()
            : base(RouteFormats.TwitterSettings)
        {
        }
    }
}