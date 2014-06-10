using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class DeleteCheckpointUrlModel : CheckpointUrlModel
    {
        public DeleteCheckpointUrlModel(string slug, string dateStr, int playerId, int checkpointId)
            : base(RouteFormats.CashgameCheckpointDelete, slug, dateStr, playerId, checkpointId)
        {
        }
    }
}