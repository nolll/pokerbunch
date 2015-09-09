using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class CurrentRankings
    {
        private readonly BunchService _bunchService;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly UserService _userService;

        public CurrentRankings(BunchService bunchService, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository, UserService userService)
        {
            _bunchService = bunchService;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
            _userService = userService;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchService.GetBySlug(request.Slug);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequirePlayer(user, player);
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
            public string UserName { get; private set; }
            public string Slug { get; private set; }

            public Request(string userName, string slug)
            {
                UserName = userName;
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
                Rank = index;
                PlayerId = totalResult.Player.Id;
                Name = totalResult.Player.DisplayName;
                TotalWinnings = new MoneyResult(totalResult.Winnings, currency);
                LastGameWinnings = lastGameResult != null ? new MoneyResult(lastGameResult.Winnings, currency) : null;
            }
        }
    }
}