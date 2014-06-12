using Web.Routing;

namespace Web.Models.UrlModels
{
    public class TwitterStartShareUrl : Url
    {
        public TwitterStartShareUrl()
            : base(RouteFormats.TwitterStartShare)
        {
        }
    }
}