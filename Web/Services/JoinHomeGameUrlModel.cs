using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class JoinHomeGameUrlModel : HomegameUrlModel
    {
        public JoinHomeGameUrlModel(string slug)
            : base(RouteFormats.HomegameJoin, slug)
        {
        }
    }
}