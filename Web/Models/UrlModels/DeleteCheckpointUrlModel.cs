using Web.Routing;

namespace Web.Models.UrlModels
{
    public class DeleteCheckpointUrlModel : CheckpointUrlModel
    {
        public DeleteCheckpointUrlModel(string slug, string dateStr, int playerId, int checkpointId)
            : base(RouteFormats.CashgameCheckpointDelete, slug, dateStr, playerId, checkpointId)
        {
        }
    }
}