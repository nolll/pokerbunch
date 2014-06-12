using Web.Routing;

namespace Web.Models.UrlModels
{
    public class JoinHomeGameUrl : HomegameUrl
    {
        public JoinHomeGameUrl(string slug)
            : base(RouteFormats.HomegameJoin, slug)
        {
        }
    }
}