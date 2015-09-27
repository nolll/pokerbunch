using Web.Common.Routes;

namespace Web.Urls
{
    public class AddPlayerConfirmationUrl : SlugUrl
    {
        public AddPlayerConfirmationUrl(string slug)
            : base(WebRoutes.PlayerAddConfirmation, slug)
        {
        }
    }
}