using Web.Routing;

namespace Web.Models.UrlModels
{
    public class HomegameDetailsUrlModel : HomegameUrlModel
    {
        public HomegameDetailsUrlModel(string slug)
            : base(RouteFormats.HomegameDetails, slug)
        {
        }
    }
}