namespace Core.UseCases.CashgameDetailsChart
{
    public class CashgameDetailsChartRequest
    {
        public string Slug { get; private set; }
        public string DateStr { get; private set; }

        public CashgameDetailsChartRequest(string slug, string dateStr = null)
        {
            Slug = slug;
            DateStr = dateStr;
        }
    }
}