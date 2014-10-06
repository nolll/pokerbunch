namespace Core.Urls
{
    public abstract class CheckpointUrl : Url
    {
        protected CheckpointUrl(string format, string slug, string dateStr, int playerId, int checkpointId)
            : base(string.Format(format, slug, dateStr, playerId, checkpointId))
        {
        }
    }
}