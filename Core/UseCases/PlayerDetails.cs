﻿using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class PlayerDetails
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IUserRepository _userRepository;

        public PlayerDetails(IBunchRepository bunchRepository, IPlayerRepository playerRepository, ICashgameRepository cashgameRepository, IUserRepository userRepository)
        {
            _bunchRepository = bunchRepository;
            _playerRepository = playerRepository;
            _cashgameRepository = cashgameRepository;
            _userRepository = userRepository;
        }

        public Result Execute(Request request)
        {
            var player = _playerRepository.Get(request.PlayerId);
            var bunch = _bunchRepository.Get(player.BunchId);
            var user = _userRepository.GetById(player.UserId);
            var isManager = RoleHandler.IsInRole(bunch.Role, Role.Manager);
            var cashgames = _cashgameRepository.PlayerList(player.Id);
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