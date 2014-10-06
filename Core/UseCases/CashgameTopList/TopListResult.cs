using System.Collections.Generic;
using System.Linq;

namespace Core.UseCases.CashgameTopList
{
    public class TopListResult
    {
        public IList<TopListItem> Items { get; private set; }
        public ToplistSortOrder OrderBy { get; private set; }
        public string Slug { get; private set; }
        public int? Year { get; private set; }

        public TopListResult(IEnumerable<TopListItem> items, ToplistSortOrder orderBy, string slug, int? year)
        {
            Items = items.ToList();
            OrderBy = orderBy;
            Slug = slug;
            Year = year;
        }
    }
}