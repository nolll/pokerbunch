using Web.Routing;

namespace Web.Models.UrlModels
{
    public class EditCheckpointUrlModel : CheckpointUrlModel
    {
        public EditCheckpointUrlModel(string slug, string dateStr, int playerId, int checkpointId)
            : base(RouteFormats.CashgameCheckpointEdit, slug, dateStr, playerId, checkpointId)
        {
        }
    }
}