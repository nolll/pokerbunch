using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class DeletePlayerUrlModel : PlayerUrlModel
    {
        public DeletePlayerUrlModel(string slug, int playerId)
            : base(RouteFormats.PlayerDelete, slug, playerId)
        {
        }
    }
}