using Web.Routing;

namespace Web.Models.UrlModels
{
    public class EditHomegameUrl : HomegameUrl
    {
        public EditHomegameUrl(string slug)
            : base(RouteFormats.HomegameEdit, slug)
        {
        }
    }
}