using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Core.UseCases.CashgameTopList
{
    public class TopListInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;

        public TopListInteractor(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
        }

        public TopListResult Execute(TopListRequest request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            return Execute(bunch, request.OrderBy, request.Year);
        }

        public TopListResult Execute(LatestTopListRequest request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var years = _cashgameRepository.GetYears(bunch.Id);
            var latestYear = years.OrderBy(o => o).Last();
            return Execute(bunch, ToplistSortOrder.Disabled, latestYear);
        }

        private TopListResult Execute(Bunch bunch, ToplistSortOrder orderBy, int? year)
        {
            var cashgames = _cashgameRepository.GetFinished(bunch.Id, year);
            var players = _playerRepository.GetList(bunch.Id).ToList();
            var suite = new CashgameSuite(cashgames, players);

            var items = suite.TotalResults.Select((o, index) => new TopListItem(bunch.Slug, o, index, bunch.Currency));
            items = SortItems(items, orderBy);

            return new TopListResult(items, orderBy, bunch.Slug, year);
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