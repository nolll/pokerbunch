using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Core.UseCases.CashgameCurrentRankings
{
    public class CurrentRankingsInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;

        public CurrentRankingsInteractor(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
        }

        public CurrentRankingsResult Execute(CurrentRankingsRequest request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var years = _cashgameRepository.GetYears(bunch.Id);
            var latestYear = years.Count > 0 ? years.OrderBy(o => o).Last() : (int?)null;
            var cashgames = _cashgameRepository.GetFinished(bunch.Id, latestYear);
            var players = _playerRepository.GetList(bunch.Id).ToList();
            var suite = new CashgameSuite(cashgames, players);
            var lastGame = cashgames.Last();
            var items = CreateItems(bunch, suite, lastGame);

            return new CurrentRankingsResult(items);
        }
        
        private IEnumerable<CurrentRankingsItem> CreateItems(Bunch bunch, CashgameSuite suite, Cashgame lastGame)
        {
            var items = new List<CurrentRankingsItem>();
            var index = 1;
            foreach (var totalResult in suite.TotalResults)
            {
                var lastGameResult = lastGame.GetResult(totalResult.Player.Id);
                var item = new CurrentRankingsItem(totalResult, lastGameResult, index++, bunch.Currency);
                items.Add(item);
            }
            return items;
        }
    }
}