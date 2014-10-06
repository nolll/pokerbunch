using System;

namespace Core.UseCases.CashgameList
{
    public class CashgameListRequest
    {
        public string Slug { get; private set; }
        public ListSortOrder SortOrder { get; private set; }
        public int? Year { get; private set; }

        public CashgameListRequest(string slug, string orderBy, int? year)
            : this(slug, ParseToplistSortOrder(orderBy), year)
        {
        }

        private CashgameListRequest(string slug, ListSortOrder sortOrder, int? year)
        {
            Slug = slug;
            SortOrder = sortOrder;
            Year = year;
        }

        private static ListSortOrder ParseToplistSortOrder(string s)
        {
            if (s == null)
                return ListSortOrder.Date;
            ListSortOrder sortOrder;
            return Enum.TryParse(s, true, out sortOrder) ? sortOrder : ListSortOrder.Date;
        }
    }
}