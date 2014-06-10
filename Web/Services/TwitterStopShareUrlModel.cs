using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class TwitterStopShareUrlModel : UrlModel
    {
        public TwitterStopShareUrlModel()
            : base(RouteFormats.TwitterStopShare)
        {
        }
    }
}