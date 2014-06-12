using Web.Routing;

namespace Web.Models.UrlModels
{
    public class JoinHomegameConfirmationUrl : HomegameUrl
    {
        public JoinHomegameConfirmationUrl(string slug)
            : base(RouteFormats.HomegameJoinConfirmation, slug)
        {
        }
    }
}