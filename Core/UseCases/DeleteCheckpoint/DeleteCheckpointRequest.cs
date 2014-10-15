namespace Core.UseCases.DeleteCheckpoint
{
    public class DeleteCheckpointRequest
    {
        public string Slug { get; private set; }
        public string DateStr { get; private set; }
        public int CheckpointId { get; private set; }

        public DeleteCheckpointRequest(string slug, string dateStr, int checkpointId)
        {
            Slug = slug;
            DateStr = dateStr;
            CheckpointId = checkpointId;
        }
    }
}