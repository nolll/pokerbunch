namespace Application.UseCases.CashgameContext
{
    public class CashgameContextRequest
    {
        public string Slug { get; private set; }
        public int? Year { get; private set; }

        public CashgameContextRequest(string slug, int? year = null)
        {
            Slug = slug;
            Year = year;
        }
    }
}