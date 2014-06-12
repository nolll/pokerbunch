using Web.Routing;

namespace Web.Models.UrlModels
{
    public class AddUserUrl : Url
    {
        public AddUserUrl()
            : base(RouteFormats.UserAdd)
        {
        }
    }
}