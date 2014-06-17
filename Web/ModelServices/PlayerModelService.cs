using Application.Services;
using Core.Entities;
using Core.Repositories;
using Web.ModelFactories.PlayerModelFactories;
using Web.Models.PlayerModels.Add;
using Web.Models.PlayerModels.Details;
using Web.Models.PlayerModels.Invite;
using Web.Models.PlayerModels.List;

namespace Web.ModelServices
{
    public class PlayerModelService : IPlayerModelService
    {
        private readonly IPlayerDetailsPageBuilder _playerDetailsPageBuilder;
        private readonly IPlayerListPageBuilder _playerListPageBuilder;
        private readonly IAddPlayerPageBuilder _addPlayerPageBuilder;
        private readonly IAddPlayerConfirmationPageBuilder _addPlayerConfirmationPageBuilder;
        private readonly IInvitePlayerPageBuilder _invitePlayerPageBuilder;
        private readonly IInvitePlayerConfirmationPageBuilder _invitePlayerConfirmationPageBuilder;
        private readonly IPlayerRepository _playerRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IAuth _auth;
        private readonly IHomegameRepository _homegameRepository;

        public PlayerModelService(
            IPlayerDetailsPageBuilder playerDetailsPageBuilder,
            IPlayerListPageBuilder playerListPageBuilder,
            IAddPlayerPageBuilder addPlayerPageBuilder,
            IAddPlayerConfirmationPageBuilder addPlayerConfirmationPageBuilder,
            IInvitePlayerPageBuilder invitePlayerPageBuilder,
            IInvitePlayerConfirmationPageBuilder invitePlayerConfirmationPageBuilder,
            IPlayerRepository playerRepository,
            IUserRepository userRepository,
            ICashgameRepository cashgameRepository,
            IAuth auth,
            IHomegameRepository homegameRepository)
        {
            _playerDetailsPageBuilder = playerDetailsPageBuilder;
            _playerListPageBuilder = playerListPageBuilder;
            _addPlayerPageBuilder = addPlayerPageBuilder;
            _addPlayerConfirmationPageBuilder = addPlayerConfirmationPageBuilder;
            _invitePlayerPageBuilder = invitePlayerPageBuilder;
            _invitePlayerConfirmationPageBuilder = invitePlayerConfirmationPageBuilder;
            _playerRepository = playerRepository;
            _userRepository = userRepository;
            _cashgameRepository = cashgameRepository;
            _auth = auth;
            _homegameRepository = homegameRepository;
        }

        public PlayerListPageModel GetListModel(string slug)
        {
            return _playerListPageBuilder.Build(slug);
        }

        public PlayerDetailsPageModel GetDetailsModel(string slug, int playerId)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var player = _playerRepository.GetById(playerId);
            var user = _userRepository.GetById(player.UserId);
            var cashgames = _cashgameRepository.GetPublished(homegame);
            var isManager = _auth.IsInRole(slug, Role.Manager);
            var hasPlayed = _cashgameRepository.HasPlayed(player);
            return _playerDetailsPageBuilder.Build(homegame, player, user, cashgames, isManager, hasPlayed);
        }

        public AddPlayerPageModel GetAddModel(string slug, AddPlayerPostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            return _addPlayerPageBuilder.Build(homegame, postModel);
        }

        public AddPlayerConfirmationPageModel GetAddConfirmationModel(string slug)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            return _addPlayerConfirmationPageBuilder.Build(homegame);
        }

        public InvitePlayerPageModel GetInviteModel(string slug, InvitePlayerPostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            return _invitePlayerPageBuilder.Build(homegame, postModel);
        }

        public InvitePlayerConfirmationPageModel GetInviteConfirmationModel(string slug)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            return _invitePlayerConfirmationPageBuilder.Build(homegame);
        }

    }
}