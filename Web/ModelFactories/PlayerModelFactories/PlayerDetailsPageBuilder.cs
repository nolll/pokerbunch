﻿using Application.Services;
using Application.Urls;
using Application.UseCases.BunchContext;
using Core.Entities;
using Core.Repositories;
using Web.ModelFactories.MiscModelFactories;
using Web.Models.PageBaseModels;
using Web.Models.PlayerModels.Details;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class PlayerDetailsPageBuilder : IPlayerDetailsPageBuilder
    {
        private readonly IAvatarModelFactory _avatarModelFactory;
        private readonly IPlayerFactsModelFactory _playerFactsModelFactory;
        private readonly IPlayerBadgesModelFactory _playerBadgesModelFactory;
        private readonly IHomegameRepository _homegameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IAuth _auth;
        private readonly IBunchContextInteractor _bunchContextInteractor;

        public PlayerDetailsPageBuilder(
            IAvatarModelFactory avatarModelFactory,
            IPlayerFactsModelFactory playerFactsModelFactory,
            IPlayerBadgesModelFactory playerBadgesModelFactory,
            IHomegameRepository homegameRepository,
            IPlayerRepository playerRepository,
            IUserRepository userRepository,
            ICashgameRepository cashgameRepository,
            IAuth auth,
            IBunchContextInteractor bunchContextInteractor)
        {
            _avatarModelFactory = avatarModelFactory;
            _playerFactsModelFactory = playerFactsModelFactory;
            _playerBadgesModelFactory = playerBadgesModelFactory;
            _homegameRepository = homegameRepository;
            _playerRepository = playerRepository;
            _userRepository = userRepository;
            _cashgameRepository = cashgameRepository;
            _auth = auth;
            _bunchContextInteractor = bunchContextInteractor;
        }

        public PlayerDetailsPageModel Build(string slug, int playerId)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var player = _playerRepository.GetById(playerId);
            var user = _userRepository.GetById(player.UserId);
            var cashgames = _cashgameRepository.GetPublished(homegame);
            var isManager = _auth.IsInRole(slug, Role.Manager);
            var hasPlayed = _cashgameRepository.HasPlayed(player);
            
            var hasUser = user != null;
            var userUrl = hasUser ? new UserDetailsUrl(user.UserName) : null;
            var userEmail = hasUser ? user.Email : null;
            var avatarModel = hasUser ? _avatarModelFactory.Create(user.Email) : null;
            var invitationUrl = new InvitePlayerUrl(homegame.Slug, player.Id);

            var contextResult = _bunchContextInteractor.Execute(new BunchContextRequest {Slug = slug});

            return new PlayerDetailsPageModel
                {
                    BrowserTitle = "Player Details",
                    PageProperties = new PageProperties(contextResult),
                    DisplayName = player.DisplayName,
                    DeleteUrl = new DeletePlayerUrl(homegame.Slug, player.Id),
                    DeleteEnabled = isManager && !hasPlayed,
                    ShowUserInfo = hasUser,
                    PlayerFactsModel = _playerFactsModelFactory.Create(homegame.Currency, cashgames, player),
                    PlayerBadgesModel = _playerBadgesModelFactory.Create(player.Id, cashgames),
                    UserUrl = userUrl,
                    UserEmail = userEmail,
                    AvatarModel = avatarModel,
                    InvitationUrl = invitationUrl
                };
        }
    }
}