namespace Core.UseCases.CashgameTopList
{
    public class LatestTopListRequest
    {
        public string Slug { get; private set; }

        public LatestTopListRequest(string slug)
        {
            Slug = slug;
        }
    }
}