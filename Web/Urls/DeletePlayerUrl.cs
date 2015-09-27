using Web.Common.Routes;

namespace Web.Urls
{
    public class DeletePlayerUrl : IdUrl
    {
        public DeletePlayerUrl(int playerId)
            : base(WebRoutes.PlayerDelete, playerId)
        {
        }
    }
}