namespace Core.UseCases.CashgameChart
{
    public class CashgameChartRequest
    {
        public string Slug { get; private set; }
        public int? Year { get; private set; }

        public CashgameChartRequest(string slug, int? year)
        {
            Slug = slug;
            Year = year;
        }
    }
}