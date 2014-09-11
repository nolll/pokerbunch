using System.Collections.Generic;
using System.Linq;
using Core.Repositories;

namespace Application.UseCases.CashgameList
{
    public class CashgameListInteractor
    {
        public static CashgameListResult Execute(
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            CashgameListRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var cashgames = cashgameRepository.GetPublished(bunch, request.Year);
            var list = cashgames.Select(o => new CashgameItem(bunch.Slug, o));
            list = SortItems(list, request.SortOrder);

            return new CashgameListResult(request.Slug, list.ToList(), request.SortOrder, request.Year);
        }

        private static IEnumerable<CashgameItem> SortItems(IEnumerable<CashgameItem> items, ListSortOrder orderBy)
        {
            switch (orderBy)
            {
                case ListSortOrder.PlayerCount:
                    return items.OrderByDescending(o => o.PlayerCount);
                case ListSortOrder.Location:
                    return items.OrderByDescending(o => o.Location);
                case ListSortOrder.Duration:
                    return items.OrderByDescending(o => o.Duration);
                case ListSortOrder.Turnover:
                    return items.OrderByDescending(o => o.Turnover);
                case ListSortOrder.AverageBuyin:
                    return items.OrderByDescending(o => o.AverageBuyin);
                default:
                    return items.OrderByDescending(o => o.Date);
            }
        }
    }
}