﻿using System;
using System.Collections.Generic;
using System.Linq;
using Application.UseCases.CashgameFacts;
using Core.Classes;
using Core.Repositories;
using Core.Services.Interfaces;

namespace Application.UseCases.CashgameTopList
{
    public class CashgameTopListInteractor : ICashgameTopListInteractor
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameService _cashgameService;

        public CashgameTopListInteractor(
            IHomegameRepository homegameRepository,
            ICashgameService cashgameService)
        {
            _homegameRepository = homegameRepository;
            _cashgameService = cashgameService;
        }

        public CashgameTopListResult Execute(CashgameTopListRequest request)
        {
            if(request.Slug == null)
                throw new ArgumentException("No slug provided");

            var homegame = _homegameRepository.GetBySlug(request.Slug);
            var suite = _cashgameService.GetSuite(homegame, request.Year);
            var results = suite.TotalResults.OrderByDescending(o => o.Winnings);
            var items = results.Select((o, index) => CreateItem(o, index, homegame.Currency, suite.Players));
            items = SortItems(items, request.OrderBy);

            return new CashgameTopListResult
                {
                    Items = items.ToList(),
                    OrderBy = request.OrderBy,
                    Slug = request.Slug,
                    Year = request.Year
                };
        }

        private TopListItem CreateItem(CashgameTotalResult totalResult, int index, CurrencySettings currency, IList<Player> players)
        {
            return new TopListItem
                {
                    Buyin = new Money(totalResult.Buyin, currency),
                    Cashout = new Money(totalResult.Cashout, currency),
                    GamesPlayed = totalResult.GameCount,
                    MinutesPlayed = TimeSpan.FromMinutes(totalResult.TimePlayed),
                    Name = GetPlayerName(players, totalResult.PlayerId),
                    Rank = index + 1,
                    Winnings = new Money(totalResult.Winnings),
                    WinRate = new Money(totalResult.WinRate)
                };
        }

        private IEnumerable<TopListItem> SortItems(IEnumerable<TopListItem> items, ToplistSortOrder orderBy)
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
                    return items.OrderByDescending(o => o.MinutesPlayed).ToList();
                case ToplistSortOrder.GamesPlayed:
                    return items.OrderByDescending(o => o.GamesPlayed).ToList();
                default:
                    return items.OrderByDescending(o => o.Winnings).ToList();
            }
        }

        private string GetPlayerName(IList<Player> players, int id)
        {
            var player = players.FirstOrDefault(o => o.Id == id);
            return player != null ? player.DisplayName : "";
        }
    }
}