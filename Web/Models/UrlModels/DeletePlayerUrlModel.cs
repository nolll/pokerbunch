using Web.Routing;

namespace Web.Models.UrlModels
{
    public class DeletePlayerUrlModel : PlayerUrlModel
    {
        public DeletePlayerUrlModel(string slug, int playerId)
            : base(RouteFormats.PlayerDelete, slug, playerId)
        {
        }
    }
}