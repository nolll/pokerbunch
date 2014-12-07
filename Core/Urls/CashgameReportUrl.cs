namespace Core.Urls
{
    public class CashgameReportUrl : BunchUrl
    {
        public CashgameReportUrl(string slug)
            : base(RouteFormats.CashgameReport, slug)
        {
        }
    }
}