using System.Collections.Generic;

namespace Application.UseCases.CashgameList
{
    public class CashgameListResult
    {
        public IList<CashgameItem> List { get; private set; }
        public ListSortOrder SortOrder { get; private set; }
        public string Slug { get; private set; }
        public int? Year { get; private set; }
        public bool ShowYear { get; private set; }
        public bool SpansMultipleYears { get; private set; }

        public CashgameListResult(string slug, IList<CashgameItem> list, ListSortOrder sortOrder, int? year, bool spansMultipleYears)
        {
            Slug = slug;
            List = list;
            SortOrder = sortOrder;
            Year = year;
            ShowYear = year.HasValue;
            SpansMultipleYears = spansMultipleYears;
        }
    }
}