using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Core.UseCases
{
    public class CurrentRankings
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;

        public CurrentRankings(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var years = _cashgameRepository.GetYears(bunch.Id);
            var latestYear = years.Count > 0 ? years.OrderBy(o => o).Last() : (int?)null;
            var cashgames = _cashgameRepository.GetFinished(bunch.Id, latestYear);
            var players = _playerRepository.GetList(bunch.Id).ToList();
            var suite = new CashgameSuite(cashgames, players);
            var lastGame = cashgames.Last();
            var items = CreateItems(bunch, suite, lastGame);

            return new Result(items);
        }
        
        private IEnumerable<Item> CreateItems(Bunch bunch, CashgameSuite suite, Cashgame lastGame)
        {
            var items = new List<Item>();
            var index = 1;
            foreach (var totalResult in suite.TotalResults)
            {
                var lastGameResult = lastGame.GetResult(totalResult.Player.Id);
                var item = new Item(totalResult, lastGameResult, index++, bunch.Currency);
                items.Add(item);
            }
            return items;
        }

        public class Request
        {
            public string Slug { get; private set; }

            public Request(string slug)
            {
                Slug = slug;
            }
        }

        public class Result
        {
            public IList<Item> Items { get; private set; }

            public Result(IEnumerable<Item> items)
            {
                Items = items.ToList();
            }
        }

        public class Item
        {
            public int Rank { get; private set; }
            public int PlayerId { get; private set; }
            public string Name { get; private set; }
            public Money TotalWinnings { get; private set; }
            public Money LastGameWinnings { get; private set; }

            public Item(CashgameTotalResult totalResult, CashgameResult lastGameResult, int index, Currency currency)
            {
                Rank = index + 1;
                PlayerId = totalResult.Player.Id;
                Name = totalResult.Player.DisplayName;
                TotalWinnings = new MoneyResult(totalResult.Winnings, currency);
                LastGameWinnings = lastGameResult != null ? new MoneyResult(lastGameResult.Winnings, currency) : null;
            }
        }
    }
}