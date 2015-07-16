namespace Core.UseCases.CashgameCurrentRankings
{
    public class CurrentRankingsRequest
    {
        public string Slug { get; private set; }

        public CurrentRankingsRequest(string slug)
        {
            Slug = slug;
        }
    }
}