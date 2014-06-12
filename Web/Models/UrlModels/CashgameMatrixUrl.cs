using Web.Routing;

namespace Web.Models.UrlModels
{
    public class CashgameMatrixUrl : HomegameWithOptionalYearUrl
    {
        public CashgameMatrixUrl(string slug, int? year)
            : base(RouteFormats.CashgameMatrix, RouteFormats.CashgameMatrixWithYear, slug, year)
        {
        }
    }
}