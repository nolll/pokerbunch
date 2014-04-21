﻿using System.Linq;
using Core.Classes;
using Core.Repositories;

namespace Core.UseCases.BunchList
{
    public class BunchListInteractor : IBunchListInteractor
    {
        private readonly IHomegameRepository _homegameRepository;

        public BunchListInteractor(
            IHomegameRepository homegameRepository)
        {
            _homegameRepository = homegameRepository;
        }

        public BunchListResult Execute()
        {
            var homegames = _homegameRepository.GetList();
            var itemList = homegames.Select(CreateItem).ToList();
            return new BunchListResult
                {
                    Bunches = itemList
                };
        }

        private BunchListItem CreateItem(Homegame homegame)
        {
            return new BunchListItem
                {
                    DisplayName = homegame.DisplayName,
                    Slug = homegame.Slug
                };
        }
    }
}