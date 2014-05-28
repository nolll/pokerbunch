﻿using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Application.UseCases.PlayerList
{
    public class PlayerListInteractor : IPlayerListInteractor
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly IPlayerRepository _playerRepository;

        public PlayerListInteractor(
            IHomegameRepository homegameRepository,
            IPlayerRepository playerRepository)
        {
            _homegameRepository = homegameRepository;
            _playerRepository = playerRepository;
        }

        public PlayerListResult Execute(PlayerListRequest request)
        {
            var homegame = _homegameRepository.GetBySlug(request.Slug);
            var players = _playerRepository.GetList(homegame);

            return new PlayerListResult
                {
                    Slug = request.Slug,
                    Players = players.Select(CreatePlayerListItem).OrderBy(o => o.Name).ToList()
                };
        }

        private PlayerListItem CreatePlayerListItem(Player player)
        {
            return new PlayerListItem
                {
                    Id = player.Id,
                    Name = player.DisplayName
                };
        } 
    }
}