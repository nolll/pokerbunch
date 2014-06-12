using Web.Routing;

namespace Web.Models.UrlModels
{
    public class HomegameDetailsUrl : HomegameUrl
    {
        public HomegameDetailsUrl(string slug)
            : base(RouteFormats.HomegameDetails, slug)
        {
        }
    }
}