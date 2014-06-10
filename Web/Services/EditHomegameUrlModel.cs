using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class EditHomegameUrlModel : HomegameUrlModel
    {
        public EditHomegameUrlModel(string slug)
            : base(RouteFormats.HomegameEdit, slug)
        {
        }
    }
}