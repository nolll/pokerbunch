namespace Application.UseCases.EditCheckpointForm
{
    public class EditCheckpointFormRequest
    {
        public string Slug { get; private set; }
        public int CheckpointId { get; private set; }
        public string DateString { get; private set; }
        public int PlayerId { get; private set; }

        public EditCheckpointFormRequest(string slug, string dateString, int playerId, int checkpointId)
        {
            Slug = slug;
            CheckpointId = checkpointId;
            DateString = dateString;
            PlayerId = playerId;
        }
    }
}