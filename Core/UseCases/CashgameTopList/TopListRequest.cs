using System;

namespace Core.UseCases.CashgameTopList
{
    public class TopListRequest
    {
        public string Slug { get; private set; }
        public ToplistSortOrder OrderBy { get; private set; }
        public int? Year { get; private set; }

        public TopListRequest(string slug, string orderBy, int? year)
            : this(slug, ParseToplistSortOrder(orderBy), year)
        {
        }

        private TopListRequest(string slug, ToplistSortOrder orderBy, int? year)
        {
            Slug = slug;
            OrderBy = orderBy;
            Year = year;
        }

        private static ToplistSortOrder ParseToplistSortOrder(string s)
        {
            if (s == null)
                return ToplistSortOrder.Winnings;
            ToplistSortOrder sortOrder;
            return Enum.TryParse(s, true, out sortOrder) ? sortOrder : ToplistSortOrder.Winnings;
        }
    }
}