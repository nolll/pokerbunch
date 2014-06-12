using Web.Routing;

namespace Web.Models.UrlModels
{
    public class DeletePlayerUrl : PlayerUrl
    {
        public DeletePlayerUrl(string slug, int playerId)
            : base(RouteFormats.PlayerDelete, slug, playerId)
        {
        }
    }
}