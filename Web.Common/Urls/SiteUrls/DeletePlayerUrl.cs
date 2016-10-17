using Web.Common.Routes;

namespace Web.Common.Urls.SiteUrls
{
    public class DeletePlayerUrl : IdUrl
    {
        public DeletePlayerUrl(string playerId)
            : base(WebRoutes.Player.Delete, playerId)
        {
        }
    }
}