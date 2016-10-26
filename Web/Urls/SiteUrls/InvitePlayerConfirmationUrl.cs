using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class InvitePlayerConfirmationUrl : IdUrl
    {
        public InvitePlayerConfirmationUrl(string playerId)
            : base(WebRoutes.Player.InviteConfirmation, playerId)
        {
        }
    }
}