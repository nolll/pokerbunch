﻿using System.Collections.Generic;
using System.Linq;
using Core.Entities;

namespace Application.UseCases.CashgameTopList
{
    public class TopListResult
    {
        public IList<TopListItem> Items { get; private set; }
        public ToplistSortOrder OrderBy { get; private set; }
        public string Slug { get; private set; }
        public int? Year { get; private set; }

        public TopListResult(Homegame homegame, IEnumerable<CashgameTotalResult> results, ToplistSortOrder sortOrder, int? year)
        {
            var sortedResults = results.OrderByDescending(o => o.Winnings);
            var items = sortedResults.Select((o, index) => new TopListItem(o, index, homegame.Currency));
            
            Items = SortItems(items, sortOrder);
            OrderBy = sortOrder;
            Slug = homegame.Slug;
            Year = year;
        }

        public static IList<TopListItem> SortItems(IEnumerable<TopListItem> items, ToplistSortOrder orderBy)
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

    }
}