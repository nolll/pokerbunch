using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class AddPlayerConfirmationUrl : SlugUrl
    {
        public AddPlayerConfirmationUrl(string slug)
            : base(WebRoutes.Player.AddConfirmation, slug)
        {
        }
    }
}