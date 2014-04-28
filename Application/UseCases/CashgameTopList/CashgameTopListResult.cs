﻿using System.Collections.Generic;

namespace Application.UseCases.CashgameTopList
{
    public class CashgameTopListResult
    {
        public IList<TopListItem> Items { get; set; }
        public ToplistSortOrder OrderBy { get; set; }
        public string Slug { get; set; }
        public int? Year { get; set; }
    }
}