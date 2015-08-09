namespace Core.UseCases.DeletePlayer
{
    public class DeletePlayerResult
    {
        public bool Deleted { get; private set; }
        public string Slug { get; private set; }
        public int PlayerId { get; private set; }

        public DeletePlayerResult(bool deleted, string slug, int playerId)
        {
            Deleted = deleted;
            Slug = slug;
            PlayerId = playerId;
        }
    }
}