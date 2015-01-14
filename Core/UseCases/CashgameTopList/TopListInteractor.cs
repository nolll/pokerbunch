using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Core.UseCases.CashgameTopList
{
    public static class TopListInteractor
    {
        public static TopListResult Execute(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository, TopListRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var players = playerRepository.GetList(bunch.Id).OrderBy(o => o.DisplayName).ToList();
            var cashgames = cashgameRepository.GetFinished(bunch.Id, request.Year);
            var suite = new CashgameSuite(cashgames, players);

            var items = suite.TotalResults.Select((o, index) => new TopListItem(bunch.Slug, o, index, bunch.Currency));
            items = SortItems(items, request.OrderBy);

            return new TopListResult(items, request.OrderBy, bunch.Slug, request.Year);
        }

        private static IEnumerable<TopListItem> SortItems(IEnumerable<TopListItem> items, ToplistSortOrder orderBy)
        {
            switch (orderBy)
            {
                case ToplistSortOrder.WinRate:
                    return items.OrderByDescending(o => o.WinRate);
                case ToplistSortOrder.Buyin:
                    return items.OrderByDescending(o => o.Buyin);
                case ToplistSortOrder.Cashout:
                    return items.OrderByDescending(o => o.Cashout);
                case ToplistSortOrder.TimePlayed:
                    return items.OrderByDescending(o => o.TimePlayed);
                case ToplistSortOrder.GamesPlayed:
                    return items.OrderByDescending(o => o.GamesPlayed);
                default:
                    return items.OrderByDescending(o => o.Winnings);
            }
        }
    }
}