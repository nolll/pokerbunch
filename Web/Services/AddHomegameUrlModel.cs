using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class AddHomegameUrlModel : UrlModel
    {
        public AddHomegameUrlModel()
            : base(RouteFormats.HomegameAdd)
        {
        }
    }
}