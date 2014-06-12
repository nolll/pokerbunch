using Web.Routing;

namespace Web.Models.UrlModels
{
    public class HomeUrl : Url
    {
        public HomeUrl()
            : base(RouteFormats.Home)
        {
        }
    }
}