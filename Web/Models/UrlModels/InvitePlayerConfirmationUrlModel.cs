using Web.Routing;

namespace Web.Models.UrlModels
{
    public class InvitePlayerConfirmationUrlModel : PlayerUrlModel
    {
        public InvitePlayerConfirmationUrlModel(string slug, int playerId)
            : base(RouteFormats.PlayerInviteConfirmation, slug, playerId)
        {
        }
    }
}