using Web.Routing;

namespace Web.Models.UrlModels
{
    public class TwitterStopShareUrl : Url
    {
        public TwitterStopShareUrl()
            : base(RouteFormats.TwitterStopShare)
        {
        }
    }
}