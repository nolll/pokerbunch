using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases.CashgameList
{
    public class CashgameListInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public CashgameListInteractor(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public CashgameListResult Execute(CashgameListRequest request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var cashgames = _cashgameRepository.GetFinished(bunch.Id, request.Year);
            cashgames = SortItems(cashgames, request.SortOrder).ToList();
            var spansMultipleYears = CashgameService.SpansMultipleYears(cashgames);
            var list = cashgames.Select(o => new CashgameItem(bunch, o));

            return new CashgameListResult(request.Slug, list.ToList(), request.SortOrder, request.Year, spansMultipleYears);
        }

        private static IEnumerable<Cashgame> SortItems(IEnumerable<Cashgame> items, ListSortOrder orderBy)
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
                    return items.OrderByDescending(o => o.StartTime);
            }
        }
    }
}