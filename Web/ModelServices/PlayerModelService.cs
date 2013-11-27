﻿using Core.Classes;
using Core.Repositories;
using Web.ModelFactories.HomegameModelFactories;
using Web.ModelFactories.PlayerModelFactories;
using Web.Models.PlayerModels.Add;
using Web.Models.PlayerModels.Details;
using Web.Models.PlayerModels.Invite;
using Web.Models.PlayerModels.Listing;

namespace Web.ModelServices
{
    public class PlayerModelService : IPlayerModelService
    {
        private readonly IPlayerDetailsPageModelFactory _playerDetailsPageModelFactory;
        private readonly IPlayerListingPageModelFactory _playerListingPageModelFactory;
        private readonly IAddPlayerPageModelFactory _addPlayerPageModelFactory;
        private readonly IAddPlayerConfirmationPageModelFactory _addPlayerConfirmationPageModelFactory;
        private readonly IInvitePlayerPageModelFactory _invitePlayerPageModelFactory;
        private readonly IInvitePlayerConfirmationPageModelFactory _invitePlayerConfirmationPageModelFactory;
        private readonly IPlayerRepository _playerRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IUserContext _userContext;

        public PlayerModelService(
            IPlayerDetailsPageModelFactory playerDetailsPageModelFactory,
            IPlayerListingPageModelFactory playerListingPageModelFactory,
            IAddPlayerPageModelFactory addPlayerPageModelFactory,
            IAddPlayerConfirmationPageModelFactory addPlayerConfirmationPageModelFactory,
            IInvitePlayerPageModelFactory invitePlayerPageModelFactory,
            IInvitePlayerConfirmationPageModelFactory invitePlayerConfirmationPageModelFactory,
            IPlayerRepository playerRepository,
            IUserRepository userRepository,
            ICashgameRepository cashgameRepository,
            IUserContext userContext)
        {
            _playerDetailsPageModelFactory = playerDetailsPageModelFactory;
            _playerListingPageModelFactory = playerListingPageModelFactory;
            _addPlayerPageModelFactory = addPlayerPageModelFactory;
            _addPlayerConfirmationPageModelFactory = addPlayerConfirmationPageModelFactory;
            _invitePlayerPageModelFactory = invitePlayerPageModelFactory;
            _invitePlayerConfirmationPageModelFactory = invitePlayerConfirmationPageModelFactory;
            _playerRepository = playerRepository;
            _userRepository = userRepository;
            _cashgameRepository = cashgameRepository;
            _userContext = userContext;
        }

        public PlayerListingPageModel GetListingModel(Homegame homegame)
        {
            var isInManagerMode = _userContext.IsInRole(homegame, Role.Manager);
            var players = _playerRepository.GetAll(homegame);
            return _playerListingPageModelFactory.Create(_userContext.GetUser(), homegame, players, isInManagerMode);
        }

        public PlayerDetailsPageModel GetDetailsModel(Homegame homegame, string playerName)
        {
            var currentUser = _userContext.GetUser();
            var player = _playerRepository.GetByName(homegame, playerName);
            var user = _userRepository.GetById(player.UserId);
            var cashgames = _cashgameRepository.GetPublished(homegame);
            var isManager = _userContext.IsInRole(homegame, Role.Manager);
            var hasPlayed = _cashgameRepository.HasPlayed(player);
            return _playerDetailsPageModelFactory.Create(currentUser, homegame, player, user, cashgames, isManager, hasPlayed);
        }

        public AddPlayerPageModel GetAddModel(Homegame homegame, AddPlayerPostModel postModel)
        {
            return _addPlayerPageModelFactory.Create(_userContext.GetUser(), homegame, postModel);
        }

        public AddPlayerConfirmationPageModel GetAddConfirmationModel(Homegame homegame)
        {
            return _addPlayerConfirmationPageModelFactory.Create(_userContext.GetUser(), homegame);
        }

        public InvitePlayerPageModel GetInviteModel(Homegame homegame)
        {
            return _invitePlayerPageModelFactory.Create(_userContext.GetUser(), homegame);
        }

        public InvitePlayerConfirmationPageModel GetInviteConfirmationModel(Homegame homegame)
        {
            return _invitePlayerConfirmationPageModelFactory.Create(_userContext.GetUser(), homegame);
        }

    }
}