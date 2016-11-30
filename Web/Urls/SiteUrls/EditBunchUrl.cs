using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class EditBunchUrl : SlugUrl
    {
        public EditBunchUrl(string slug)
            : base(WebRoutes.Bunch.Edit, slug)
        {
        }
    }
}