using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class InvitePlayerUrlModel : PlayerUrlModel
    {
        public InvitePlayerUrlModel(string slug, int playerId)
            : base(RouteFormats.PlayerInvite, slug, playerId)
        {
        }
    }
}