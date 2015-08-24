using Core.Urls;

namespace Web.Urls
{
    public class AddPlayerConfirmationUrl : SlugUrl
    {
        public AddPlayerConfirmationUrl(string slug)
            : base(Routes.PlayerAddConfirmation, slug)
        {
        }
    }
}