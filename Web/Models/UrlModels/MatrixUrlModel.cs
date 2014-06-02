using Web.Routing;

namespace Web.Models.UrlModels
{
    public class MatrixUrlModel : HomegameWithOptionalYearUrlModel
    {
        public MatrixUrlModel(string slug, int? year)
            : base(RouteFormats.CashgameMatrix, RouteFormats.CashgameMatrixWithYear, slug, year)
        {
        }
    }
}