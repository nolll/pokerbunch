using Web.Common.Routes;

namespace Web.Common.Urls.SiteUrls
{
    public class DeleteCheckpointUrl : IdUrl
    {
        public DeleteCheckpointUrl(string id)
            : base(WebRoutes.Cashgame.CheckpointDelete, id)
        {
        }
    }
}