using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class TwitterSettingsUrlModel : UrlModel
    {
        public TwitterSettingsUrlModel()
            : base(RouteFormats.TwitterSettings)
        {
        }
    }
}