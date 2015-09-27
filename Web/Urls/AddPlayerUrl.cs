using Web.Common.Routes;

namespace Web.Urls
{
    public class AddPlayerUrl : SlugUrl
    {
        public AddPlayerUrl(string slug)
            : base(WebRoutes.PlayerAdd, slug)
        {
        }
    }
}