using System.Linq;
using Application.Services;
using Core.Entities;
using Core.Repositories;
using Web.ModelFactories.PlayerModelFactories;
using Web.Models.PlayerModels.Add;
using Web.Models.PlayerModels.Details;
using Web.Models.PlayerModels.Invite;
using Web.Models.PlayerModels.List;
using Web.Security;

namespace Web.ModelServices
{
    public class PlayerModelService : IPlayerModelService
    {
        private readonly IPlayerDetailsPageModelFactory _playerDetailsPageModelFactory;
        private readonly IPlayerListPageBuilder _playerListPageBuilder;
        private readonly IAddPlayerPageModelFactory _addPlayerPageModelFactory;
        private readonly IAddPlayerConfirmationPageModelFactory _addPlayerConfirmationPageModelFactory;
        private readonly IInvitePlayerPageModelFactory _invitePlayerPageModelFactory;
        private readonly IInvitePlayerConfirmationPageModelFactory _invitePlayerConfirmationPageModelFactory;
        private readonly IPlayerRepository _playerRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IAuth _auth;
        private readonly IHomegameRepository _homegameRepository;

        public PlayerModelService(
            IPlayerDetailsPageModelFactory playerDetailsPageModelFactory,
            IPlayerListPageBuilder playerListPageBuilder,
            IAddPlayerPageModelFactory addPlayerPageModelFactory,
            IAddPlayerConfirmationPageModelFactory addPlayerConfirmationPageModelFactory,
            IInvitePlayerPageModelFactory invitePlayerPageModelFactory,
            IInvitePlayerConfirmationPageModelFactory invitePlayerConfirmationPageModelFactory,
            IPlayerRepository playerRepository,
            IUserRepository userRepository,
            ICashgameRepository cashgameRepository,
            IAuth auth,
            IHomegameRepository homegameRepository)
        {
            _playerDetailsPageModelFactory = playerDetailsPageModelFactory;
            _playerListPageBuilder = playerListPageBuilder;
            _addPlayerPageModelFactory = addPlayerPageModelFactory;
            _addPlayerConfirmationPageModelFactory = addPlayerConfirmationPageModelFactory;
            _invitePlayerPageModelFactory = invitePlayerPageModelFactory;
            _invitePlayerConfirmationPageModelFactory = invitePlayerConfirmationPageModelFactory;
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
            return _playerDetailsPageModelFactory.Create(homegame, player, user, cashgames, isManager, hasPlayed);
        }

        public AddPlayerPageModel GetAddModel(string slug, AddPlayerPostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            return _addPlayerPageModelFactory.Create(homegame, postModel);
        }

        public AddPlayerConfirmationPageModel GetAddConfirmationModel(string slug)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            return _addPlayerConfirmationPageModelFactory.Create(homegame);
        }

        public InvitePlayerPageModel GetInviteModel(string slug, InvitePlayerPostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            return _invitePlayerPageModelFactory.Create(homegame, postModel);
        }

        public InvitePlayerConfirmationPageModel GetInviteConfirmationModel(string slug)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            return _invitePlayerConfirmationPageModelFactory.Create(homegame);
        }

    }
}