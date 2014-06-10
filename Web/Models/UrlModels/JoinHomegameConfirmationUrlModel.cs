using Web.Routing;

namespace Web.Models.UrlModels
{
    public class JoinHomegameConfirmationUrlModel : HomegameUrlModel
    {
        public JoinHomegameConfirmationUrlModel(string slug)
            : base(RouteFormats.HomegameJoinConfirmation, slug)
        {
        }
    }
}