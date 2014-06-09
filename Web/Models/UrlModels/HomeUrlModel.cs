using Web.Routing;

namespace Web.Models.UrlModels
{
    public class HomeUrlModel : UrlModel
    {
        public HomeUrlModel()
            : base(RouteFormats.Home)
        {
        }
    }
}