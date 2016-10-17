using Web.Common.Routes;

namespace Web.Common.Urls.SiteUrls
{
    public class InvitePlayerUrl : IdUrl
    {
        public InvitePlayerUrl(string playerId)
            : base(WebRoutes.Player.Invite, playerId)
        {
        }
    }
}