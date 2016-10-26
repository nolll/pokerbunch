using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class DeleteCheckpointUrl : IdUrl
    {
        public DeleteCheckpointUrl(string id)
            : base(WebRoutes.Cashgame.CheckpointDelete, id)
        {
        }
    }
}