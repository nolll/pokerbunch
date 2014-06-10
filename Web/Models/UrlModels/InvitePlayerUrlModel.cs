using Web.Routing;

namespace Web.Models.UrlModels
{
    public class InvitePlayerUrlModel : PlayerUrlModel
    {
        public InvitePlayerUrlModel(string slug, int playerId)
            : base(RouteFormats.PlayerInvite, slug, playerId)
        {
        }
    }
}