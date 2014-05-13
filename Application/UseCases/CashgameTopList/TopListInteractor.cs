using System;
using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Core.Repositories;
using Core.Services.Interfaces;

namespace Application.UseCases.CashgameTopList
{
    public class TopListInteractor : ITopListInteractor
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameService _cashgameService;

        public TopListInteractor(
            IHomegameRepository homegameRepository,
            ICashgameService cashgameService)
        {
            _homegameRepository = homegameRepository;
            _cashgameService = cashgameService;
        }

        public TopListResult Execute(TopListRequest request)
        {
            var homegame = _homegameRepository.GetBySlug(request.Slug);
            var suite = _cashgameService.GetSuite(homegame, request.Year);
            var results = suite.TotalResults.OrderByDescending(o => o.Winnings);
            var items = results.Select((o, index) => CreateItem(o, index, homegame.Currency, suite.Players));
            items = SortItems(items, request.OrderBy);

            return new TopListResult
                {
                    Items = items.ToList(),
                    OrderBy = request.OrderBy,
                    Slug = request.Slug,
                    Year = request.Year
                };
        }

        public TopListItem CreateItem(CashgameTotalResult totalResult, int index, Currency currency, IEnumerable<Player> players)
        {
            return new TopListItem
                {
                    Buyin = new Money(totalResult.Buyin, currency),
                    Cashout = new Money(totalResult.Cashout, currency),
                    GamesPlayed = totalResult.GameCount,
                    TimePlayed = Time.FromMinutes(totalResult.TimePlayed),
                    Name = GetPlayerName(players, totalResult.PlayerId),
                    Rank = index + 1,
                    Winnings = new MoneyResult(totalResult.Winnings, currency),
                    WinRate = new MoneyWinRate(totalResult.WinRate, currency)
                };
        }

        public IList<TopListItem> SortItems(IEnumerable<TopListItem> items, ToplistSortOrder orderBy)
        {
            switch (orderBy)
            {
                case ToplistSortOrder.WinRate:
                    return items.OrderByDescending(o => o.WinRate).ToList();
                case ToplistSortOrder.Buyin:
                    return items.OrderByDescending(o => o.Buyin).ToList();
                case ToplistSortOrder.Cashout:
                    return items.OrderByDescending(o => o.Cashout).ToList();
                case ToplistSortOrder.TimePlayed:
                    return items.OrderByDescending(o => o.TimePlayed).ToList();
                case ToplistSortOrder.GamesPlayed:
                    return items.OrderByDescending(o => o.GamesPlayed).ToList();
                default:
                    return items.OrderByDescending(o => o.Winnings).ToList();
            }
        }

        private string GetPlayerName(IEnumerable<Player> players, int id)
        {
            var player = players.FirstOrDefault(o => o.Id == id);
            return player != null ? player.DisplayName : "";
        }
    }
}