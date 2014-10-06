namespace Core.UseCases.BuyinForm
{
    public class BuyinFormRequest
    {
        public string Slug { get; private set; }
        public int PlayerId { get; private set; }

        public BuyinFormRequest(string slug, int playerId)
        {
            Slug = slug;
            PlayerId = playerId;
        }
    }
}