using Web.Routing;

namespace Web.Models.UrlModels
{
    public class InvitePlayerUrl : PlayerUrl
    {
        public InvitePlayerUrl(string slug, int playerId)
            : base(RouteFormats.PlayerInvite, slug, playerId)
        {
        }
    }
}