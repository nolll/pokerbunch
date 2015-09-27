using Web.Common.Routes;

namespace Web.Urls
{
    public class EditBunchUrl : SlugUrl
    {
        public EditBunchUrl(string slug)
            : base(WebRoutes.BunchEdit, slug)
        {
        }
    }
}