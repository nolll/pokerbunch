using Web.Routing;

namespace Web.Models.UrlModels
{
    public class AddHomegameUrl : Url
    {
        public AddHomegameUrl()
            : base(RouteFormats.HomegameAdd)
        {
        }
    }
}