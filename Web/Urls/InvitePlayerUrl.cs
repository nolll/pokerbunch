using Web.Common.Routes;

namespace Web.Urls
{
    public class InvitePlayerUrl : IdUrl
    {
        public InvitePlayerUrl(int playerId)
            : base(WebRoutes.PlayerInvite, playerId)
        {
        }
    }
}