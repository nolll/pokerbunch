using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class EditCheckpointUrl : IdUrl
    {
        public EditCheckpointUrl(string checkpointId)
            : base(WebRoutes.Cashgame.CheckpointEdit, checkpointId)
        {
        }
    }
}