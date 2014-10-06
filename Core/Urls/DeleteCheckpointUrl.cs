namespace Core.Urls
{
    public class DeleteCheckpointUrl : CheckpointUrl
    {
        public DeleteCheckpointUrl(string slug, string dateStr, int playerId, int checkpointId)
            : base(RouteFormats.CashgameCheckpointDelete, slug, dateStr, playerId, checkpointId)
        {
        }
    }
}