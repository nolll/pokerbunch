using Web.Common.Routes;

namespace Web.Common.Urls.SiteUrls
{
    public class InvitePlayerUrl : IdUrl
    {
        public InvitePlayerUrl(int playerId)
            : base(WebRoutes.PlayerInvite, playerId)
        {
        }
    }
}