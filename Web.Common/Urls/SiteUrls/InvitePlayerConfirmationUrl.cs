using Web.Common.Routes;

namespace Web.Common.Urls.SiteUrls
{
    public class InvitePlayerConfirmationUrl : IdUrl
    {
        public InvitePlayerConfirmationUrl(string playerId)
            : base(WebRoutes.Player.InviteConfirmation, playerId)
        {
        }
    }
}