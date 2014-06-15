namespace Application.Urls
{
    public class EditCheckpointUrl : CheckpointUrl
    {
        public EditCheckpointUrl(string slug, string dateStr, int playerId, int checkpointId)
            : base(RouteFormats.CashgameCheckpointEdit, slug, dateStr, playerId, checkpointId)
        {
        }
    }
}