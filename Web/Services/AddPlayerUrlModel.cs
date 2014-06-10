using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class AddPlayerUrlModel : HomegameUrlModel
    {
        public AddPlayerUrlModel(string slug)
            : base(RouteFormats.PlayerAdd, slug)
        {
        }
    }
}