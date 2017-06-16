﻿using System.Linq;
using Core.Entities;
using Core.Services;

namespace Core.UseCases
{
    public class PlayerDetails
    {
        private readonly IBunchService _bunchService;
        private readonly IPlayerService _playerService;
        private readonly ICashgameService _cashgameService;
        private readonly IUserService _userService;

        public PlayerDetails(IBunchService bunchService, IPlayerService playerService, ICashgameService cashgameService, IUserService userService)
        {
            _bunchService = bunchService;
            _playerService = playerService;
            _cashgameService = cashgameService;
            _userService = userService;
        }

        public Result Execute(Request request)
        {
            var player = _playerService.Get(request.PlayerId);
            var bunch = _bunchService.Get(player.BunchId);
            var user = player.IsUser ? _userService.GetByNameOrEmail(player.UserName) : null;
            var isManager = RoleHandler.IsInRole(bunch.Role, Role.Manager);
            var cashgames = _cashgameService.PlayerList(player.Id);
            var hasPlayed = cashgames.Any();
            var avatarUrl = user != null ? GravatarService.GetAvatarUrl(user.Email) : string.Empty;

            return new Result(bunch, player, user, isManager, hasPlayed, avatarUrl);
        }

        public class Request
        {
            public string PlayerId { get; }

            public Request(string playerId)
            {
                PlayerId = playerId;
            }
        }

        public class Result
        {
            public string DisplayName { get; private set; }
            public string PlayerId { get; private set; }
            public bool CanDelete { get; private set; }
            public bool IsUser { get; private set; }
            public string UserName { get; private set; }
            public string AvatarUrl { get; private set; }
            public string Slug { get; private set; }
            public string Color { get; private set; }

            public Result(Bunch bunch, Player player, User user, bool isManager, bool hasPlayed, string avatarUrl)
            {
                var isUser = user != null;

                DisplayName = player.DisplayName;
                PlayerId = player.Id;
                CanDelete = isManager && !hasPlayed;
                IsUser = isUser;
                UserName = isUser ? user.UserName : string.Empty;
                AvatarUrl = avatarUrl;
                Color = player.Color;
                Slug = bunch.Id;
            }
        }
    }
}