using Web.Routing;

namespace Web.Models.UrlModels
{
    public class AddHomegameUrlModel : UrlModel
    {
        public AddHomegameUrlModel()
            : base(RouteFormats.HomegameAdd)
        {
        }
    }
}