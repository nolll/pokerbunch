namespace Core.Urls
{
    public class CashgameChartUrl : BunchWithOptionalYearUrl
    {
        public CashgameChartUrl(string slug, int? year)
            : base(Routes.CashgameChart, Routes.CashgameChartWithYear, slug, year)
        {
        }
    }
}