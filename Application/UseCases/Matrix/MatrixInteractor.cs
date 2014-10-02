using System;
using System.Collections.Generic;
using System.Linq;
using Application.Urls;
using Core.Entities;
using Core.Repositories;

namespace Application.UseCases.Matrix
{
    public class MatrixInteractor
    {
        public static MatrixResult Execute(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, MatrixRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var cashgames = cashgameRepository.GetPublished(bunch, null);

            var gameItems = CreateGameItems(bunch.Slug, cashgames);

            return new MatrixResult(gameItems);
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