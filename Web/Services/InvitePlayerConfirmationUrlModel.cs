using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class InvitePlayerConfirmationUrlModel : PlayerUrlModel
    {
        public InvitePlayerConfirmationUrlModel(string slug, int playerId)
            : base(RouteFormats.PlayerInviteConfirmation, slug, playerId)
        {
        }
    }
}