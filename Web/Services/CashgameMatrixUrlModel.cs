using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class CashgameMatrixUrlModel : HomegameWithOptionalYearUrlModel
    {
        public CashgameMatrixUrlModel(string slug, int? year)
            : base(RouteFormats.CashgameMatrix, RouteFormats.CashgameMatrixWithYear, slug, year)
        {
        }
    }
}