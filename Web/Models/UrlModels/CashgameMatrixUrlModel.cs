using Web.Routing;

namespace Web.Models.UrlModels
{
    public class CashgameMatrixUrlModel : HomegameWithOptionalYearUrlModel
    {
        public CashgameMatrixUrlModel(string slug, int? year)
            : base(RouteFormats.CashgameMatrix, RouteFormats.CashgameMatrixWithYear, slug, year)
        {
        }
    }
}