using Web.Common.Routes;

namespace Web.Urls
{
    public class InvitePlayerConfirmationUrl : IdUrl
    {
        public InvitePlayerConfirmationUrl(int playerId)
            : base(WebRoutes.PlayerInviteConfirmation, playerId)
        {
        }
    }
}