namespace Core.UseCases.CashgameFacts
{
    public class CashgameFactsRequest
    {
        public string Slug { get; set; }
        public int? Year { get; set; }

        public CashgameFactsRequest(string slug, int? year)
        {
            Slug = slug;
            Year = year;
        }
    }
}