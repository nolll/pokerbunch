namespace Core.Urls
{
    public class EditCheckpointUrl : CheckpointUrl
    {
        public EditCheckpointUrl(string slug, string dateStr, int playerId, int checkpointId)
            : base(Routes.CashgameCheckpointEdit, slug, dateStr, playerId, checkpointId)
        {
        }
    }
}