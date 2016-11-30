using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class DeletePlayerUrl : IdUrl
    {
        public DeletePlayerUrl(string playerId)
            : base(WebRoutes.Player.Delete, playerId)
        {
        }
    }
}