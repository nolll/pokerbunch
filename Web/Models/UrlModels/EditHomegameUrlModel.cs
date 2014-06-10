using Web.Routing;

namespace Web.Models.UrlModels
{
    public class EditHomegameUrlModel : HomegameUrlModel
    {
        public EditHomegameUrlModel(string slug)
            : base(RouteFormats.HomegameEdit, slug)
        {
        }
    }
}