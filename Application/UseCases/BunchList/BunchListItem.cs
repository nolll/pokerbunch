﻿using Core.Entities;

namespace Application.UseCases.BunchList
{
    public class BunchListItem
    {
        public string DisplayName { get; private set; }
        public string Slug { get; private set; }

        public BunchListItem(Bunch bunch)
        {
            DisplayName = bunch.DisplayName;
            Slug = bunch.Slug;
        }
    }
}