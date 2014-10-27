using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Factories;
using Core.Repositories;
using Core.Urls;

namespace Core.UseCases.Matrix
{
    public class MatrixInteractor
    {
        public static MatrixResult Execute(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository, MatrixRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var cashgames = cashgameRepository.GetFinished(bunch.Id, request.Year);
            var players = playerRepository.GetList(bunch.Id);
            var suite = CashgameSuiteFactory.Create(cashgames, players);
            
            var gameItems = CreateGameItems(bunch.Slug, cashgames);
            var playerItems = CreatePlayerItems(bunch, suite);
            var spansMultipleYears = suite.SpansMultipleYears;

            return new MatrixResult(gameItems, playerItems, spansMultipleYears);
        }

        private static IList<MatrixPlayerItem> CreatePlayerItems(Bunch bunch, CashgameSuite suite)
        {
            var index = 0;
            var playerItems = new List<MatrixPlayerItem>();
            foreach (var totalResult in suite.TotalResults)
            {
                var p = totalResult.Player;
                var rank = ++index;
                var name = p.DisplayName;
                var playerUrl = new PlayerDetailsUrl(bunch.Slug, p.Id);
                var results = CreatePlayerResultItems(bunch, suite.Cashgames, p);
                var winnings = new Money(totalResult.Winnings, bunch.Currency);
                var playerItem = new MatrixPlayerItem(rank, name, playerUrl, results, winnings);
                playerItems.Add(playerItem);
            }
            return playerItems;
        }

        private static IDictionary<int, MatrixResultItem> CreatePlayerResultItems(Bunch bunch, IList<Cashgame> cashgames, Player player)
        {
            var items = new Dictionary<int, MatrixResultItem>();
            foreach (var cashgame in cashgames)
            {
                var result = cashgame.GetResult(player.Id);
                if (result != null)
                {
                    var hasTransactions = result.Buyin > 0;
                    var buyin = new Money(result.Buyin, bunch.Currency);
                    var cashout = new Money(result.Stack, bunch.Currency);
                    var winnings = new Money(result.Winnings, bunch.Currency);
                    var hasBestResult = cashgame.IsBestResult(result);
                    var item = new MatrixResultItem(buyin, cashout, winnings, hasBestResult, hasTransactions);
                    items.Add(cashgame.Id, item);
                }
            }
            return items;
        }

        private static List<GameItem> CreateGameItems(string slug, IList<Cashgame> cashgames)
        {
            return cashgames
                .Where(o => o.StartTime.HasValue)
                .OrderByDescending(o => o.StartTime)
                .Select(o => CreateGameItem(slug, o.Id, o.StartTime.Value))
                .ToList();
        }

        private static GameItem CreateGameItem(string slug, int cashgameId, DateTime startTime)
        {
            var date = new Date(startTime);
            var url = new CashgameDetailsUrl(slug, date.IsoString);
            
            return new GameItem(cashgameId, date, url);
        }
    }
}