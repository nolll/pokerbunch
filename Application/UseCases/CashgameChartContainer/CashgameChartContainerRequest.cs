namespace Application.UseCases.CashgameChartContainer
{
    public class CashgameChartContainerRequest
    {
        public CashgameChartContainerRequest(string slug, int? year)
        {
            Slug = slug;
            Year = year;
        }

        public string Slug { get; private set; }
        public int? Year { get; private set; }
    }
}