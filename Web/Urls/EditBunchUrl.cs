using Core.Urls;

namespace Web.Urls
{
    public class EditBunchUrl : SlugUrl
    {
        public EditBunchUrl(string slug)
            : base(Routes.BunchEdit, slug)
        {
        }
    }
}