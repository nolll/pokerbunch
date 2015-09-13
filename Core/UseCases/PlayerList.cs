﻿using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class PlayerList
    {
        private readonly BunchService _bunchService;
        private readonly UserService _userService;
        private readonly PlayerService _playerService;

        public PlayerList(BunchService bunchService, UserService userService, PlayerService playerService)
        {
            _bunchService = bunchService;
            _userService = userService;
            _playerService = playerService;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchService.GetBySlug(request.Slug);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerService.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequirePlayer(user, player);
            var players = _playerService.GetList(bunch.Id);
            var isManager = RoleHandler.IsInRole(user, player, Role.Manager);

            return new Result(bunch, players, isManager);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public string Slug { get; private set; }

            public Request(string userName, string slug)
            {
                UserName = userName;
                Slug = slug;
            }
        }

        public class Result
        {
            public IList<PlayerListItem> Players { get; private set; }
            public bool CanAddPlayer { get; private set; }
            public string Slug { get; private set; }

            public Result(Bunch bunch, IEnumerable<Player> players, bool isManager)
            {
                Players = players.Select(o => new PlayerListItem(o)).OrderBy(o => o.Name).ToList();
                CanAddPlayer = isManager;
                Slug = bunch.Slug;
            }
        }

        public class PlayerListItem
        {
            public string Name { get; private set; }
            public int Id { get; private set; }

            public PlayerListItem(Player player)
            {
                Name = player.DisplayName;
                Id = player.Id;
            }
        }
    }
}