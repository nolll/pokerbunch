using Web.Routing;

namespace Web.Models.UrlModels
{
    public class JoinHomeGameUrlModel : HomegameUrlModel
    {
        public JoinHomeGameUrlModel(string slug)
            : base(RouteFormats.HomegameJoin, slug)
        {
        }
    }
}