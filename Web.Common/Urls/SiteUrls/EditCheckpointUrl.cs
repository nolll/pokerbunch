using Web.Common.Routes;

namespace Web.Common.Urls.SiteUrls
{
    public class EditCheckpointUrl : IdUrl
    {
        public EditCheckpointUrl(string checkpointId)
            : base(WebRoutes.Cashgame.CheckpointEdit, checkpointId)
        {
        }
    }
}