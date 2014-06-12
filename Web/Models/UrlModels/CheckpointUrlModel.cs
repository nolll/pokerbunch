namespace Web.Models.UrlModels
{
    public abstract class CheckpointUrlModel : UrlModel
    {
        protected CheckpointUrlModel(string format, string slug, string dateStr, int playerId, int checkpointId)
            : base(string.Format(format, slug, dateStr, playerId, checkpointId))
        {
        }
    }
}