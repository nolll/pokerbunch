using Web.Urls;

namespace Core.Urls
{
    public class ListUrl : BunchWithOptionalYearUrl
    {
        public ListUrl(string slug, int? year)
            : base(Routes.CashgameList, Routes.CashgameListWithYear, slug, year)
        {
        }
    }
}