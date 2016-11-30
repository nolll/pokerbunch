using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class InvitePlayerUrl : IdUrl
    {
        public InvitePlayerUrl(string playerId)
            : base(WebRoutes.Player.Invite, playerId)
        {
        }
    }
}