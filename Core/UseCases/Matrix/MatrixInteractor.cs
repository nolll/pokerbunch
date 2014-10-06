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
            var cashgames = cashgameRepository.GetPublished(bunch, request.Year);
            var players = playerRepository.GetList(bunch);
            var suite = CashgameSuiteFactory.Create(cashgames, players);
            
            var gameItems = CreateGameItems(bunch.Slug, cashgames);
            var playerItems = CreatePlayerItems();
            var spansMultipleYears = suite.SpansMultipleYears;

            return new MatrixResult(gameItems, playerItems, spansMultipleYears);
        }

        private static IList<MatrixPlayerItem> CreatePlayerItems()
        {
            //Rank = rank;
            //Name = player.DisplayName;
            //PlayerUrl = new PlayerDetailsUrl(bunch.Slug, player.Id);
            //Results = CreatePlayerResultItems(suite.Cashgames, player);
            //TotalResult = Globalization.FormatResult(bunch.Currency, result.Winnings);
            return new List<MatrixPlayerItem>();
        }

        private static List<GameItem> CreateGameItems(string slug, IList<Cashgame> cashgames)
        {
            return cashgames
                .Where(o => o.StartTime.HasValue)
                .OrderByDescending(o => o.StartTime)
                .Select(o => CreateGameItem(slug, o.StartTime.Value))
                .ToList();
        }

        private static GameItem CreateGameItem(string slug, DateTime startTime)
        {
            var date = new Date(startTime);
            var url = new CashgameDetailsUrl(slug, date.IsoString);
            
            return new GameItem(date, url);
        }
    }
}