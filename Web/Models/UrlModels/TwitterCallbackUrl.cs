using Web.Routing;

namespace Web.Models.UrlModels
{
    public class TwitterCallbackUrl : Url
    {
        public TwitterCallbackUrl()
            : base(RouteFormats.TwitterCallback)
        {
        }
    }
}