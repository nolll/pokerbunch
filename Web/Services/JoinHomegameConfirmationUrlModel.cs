using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class JoinHomegameConfirmationUrlModel : HomegameUrlModel
    {
        public JoinHomegameConfirmationUrlModel(string slug)
            : base(RouteFormats.HomegameJoinConfirmation, slug)
        {
        }
    }
}