using Web.Routing;

namespace Web.Models.UrlModels
{
    public class TwitterCallbackUrlModel : UrlModel
    {
        public TwitterCallbackUrlModel()
            : base(RouteFormats.TwitterCallback)
        {
        }
    }
}