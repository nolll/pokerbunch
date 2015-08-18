using Core.Urls;

namespace Web.Urls
{
    public class PlayerIndexUrl : SlugUrl
    {
        public PlayerIndexUrl(string slug)
            : base(Routes.PlayerIndex, slug)
        {
        }
    }
}