using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class TwitterStartShareUrlModel : UrlModel
    {
        public TwitterStartShareUrlModel()
            : base(RouteFormats.TwitterStartShare)
        {
        }
    }
}