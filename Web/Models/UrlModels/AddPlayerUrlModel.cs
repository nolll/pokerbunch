using Web.Routing;

namespace Web.Models.UrlModels
{
    public class AddPlayerUrlModel : HomegameUrlModel
    {
        public AddPlayerUrlModel(string slug)
            : base(RouteFormats.PlayerAdd, slug)
        {
        }
    }
}