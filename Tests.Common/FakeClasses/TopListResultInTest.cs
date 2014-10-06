using System.Collections.Generic;
using Core.UseCases.CashgameTopList;

namespace Tests.Common.FakeClasses
{
    public class TopListResultInTest : TopListResult
    {
        public TopListResultInTest(
            IList<TopListItem> items = null,
            ToplistSortOrder orderBy = default(ToplistSortOrder),
            string slug = null,
            int? year = null)
            
            : base(
                items ?? new List<TopListItem>(),
                orderBy,
                slug,
                year)
        {
        }
    }
}