﻿using System.Collections.Generic;
using System.Linq;

namespace Application.UseCases.CashgameTopList
{
    public class TopListResult
    {
        public IList<TopListItem> Items { get; private set; }
        public ToplistSortOrder OrderBy { get; private set; }
        public string Slug { get; private set; }
        public int? Year { get; private set; }

        public TopListResult(IEnumerable<TopListItem> items, ToplistSortOrder orderBy, string slug, int? year)
        {
            Items = SortItems(items, orderBy);
            OrderBy = orderBy;
            Slug = slug;
            Year = year;
        }

        private static IList<TopListItem> SortItems(IEnumerable<TopListItem> items, ToplistSortOrder orderBy)
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