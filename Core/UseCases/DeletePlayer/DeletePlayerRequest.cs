namespace Core.UseCases.DeletePlayer
{
    public class DeletePlayerRequest
    {
        public string Slug { get; private set; }
        public int PlayerId { get; private set; }

        public DeletePlayerRequest(string slug, int playerId)
        {
            PlayerId = playerId;
            Slug = slug;
        }
    }
}