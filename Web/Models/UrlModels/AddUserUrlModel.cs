using Web.Routing;

namespace Web.Models.UrlModels
{
    public class AddUserUrlModel : UrlModel
    {
        public AddUserUrlModel()
            : base(RouteFormats.UserAdd)
        {
        }
    }
}