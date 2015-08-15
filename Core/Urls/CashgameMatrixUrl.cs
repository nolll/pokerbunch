namespace Core.Urls
{
    public class CashgameMatrixUrl : BunchWithOptionalYearUrl
    {
        public CashgameMatrixUrl(string slug, int? year)
            : base(Routes.CashgameMatrix, Routes.CashgameMatrixWithYear, slug, year)
        {
        }
    }
}