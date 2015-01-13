namespace Core.UseCases.CashgameFacts
{
    public class CashgameFactsRequest
    {
        public string Slug { get; private set; }
        public int? Year { get; private set; }

        public CashgameFactsRequest(string slug, int? year)
        {
            Slug = slug;
            Year = year;
        }
    }
}