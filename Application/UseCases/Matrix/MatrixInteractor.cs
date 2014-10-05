using System;
using System.Collections.Generic;
using System.Linq;
using Application.Urls;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Application.UseCases.Matrix
{
    public class MatrixInteractor
    {
        public static MatrixResult Execute(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, MatrixRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var cashgames = cashgameRepository.GetPublished(bunch, request.Year);

            var gameItems = CreateGameItems(bunch.Slug, cashgames);
            var playerItems = CreatePlayerItems();
            var spansMultipleYears = CashgameService.SpansMultipleYears(cashgames);

            return new MatrixResult(gameItems, playerItems, spansMultipleYears);
        }

        private static IList<MatrixPlayerItem> CreatePlayerItems()
        {
            //Rank = rank;
            //Name = player.DisplayName;
            //PlayerUrl = new PlayerDetailsUrl(bunch.Slug, player.Id);
            //CellModels = CreateCells(suite.Cashgames, player);
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