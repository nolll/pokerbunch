using Web.Common.Routes;

namespace Web.Urls
{
    public class CashgameReportUrl : SlugUrl
    {
        public CashgameReportUrl(string slug)
            : base(WebRoutes.CashgameReport, slug)
        {
        }
    }
}