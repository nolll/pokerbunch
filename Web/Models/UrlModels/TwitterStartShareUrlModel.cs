using Web.Routing;

namespace Web.Models.UrlModels
{
    public class TwitterStartShareUrlModel : UrlModel
    {
        public TwitterStartShareUrlModel()
            : base(RouteFormats.TwitterStartShare)
        {
        }
    }
}