using Core.Urls;

namespace Web.Urls
{
    public class PlayerIndexUrl : BunchUrl
    {
        public PlayerIndexUrl(string slug)
            : base(Routes.PlayerIndex, slug)
        {
        }
    }
}