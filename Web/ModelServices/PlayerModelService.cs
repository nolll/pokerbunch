using System.Linq;
using Core.Classes;
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
        private readonly IPlayerListPageModelFactory _playerListPageModelFactory;
        private readonly IAddPlayerPageModelFactory _addPlayerPageModelFactory;
        private readonly IAddPlayerConfirmationPageModelFactory _addPlayerConfirmationPageModelFactory;
        private readonly IInvitePlayerPageModelFactory _invitePlayerPageModelFactory;
        private readonly IInvitePlayerConfirmationPageModelFactory _invitePlayerConfirmationPageModelFactory;
        private readonly IPlayerRepository _playerRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IAuthentication _authentication;
        private readonly IHomegameRepository _homegameRepository;

        public PlayerModelService(
            IPlayerDetailsPageModelFactory playerDetailsPageModelFactory,
            IPlayerListPageModelFactory playerListPageModelFactory,
            IAddPlayerPageModelFactory addPlayerPageModelFactory,
            IAddPlayerConfirmationPageModelFactory addPlayerConfirmationPageModelFactory,
            IInvitePlayerPageModelFactory invitePlayerPageModelFactory,
            IInvitePlayerConfirmationPageModelFactory invitePlayerConfirmationPageModelFactory,
            IPlayerRepository playerRepository,
            IUserRepository userRepository,
            ICashgameRepository cashgameRepository,
            IAuthentication authentication,
            IHomegameRepository homegameRepository)
        {
            _playerDetailsPageModelFactory = playerDetailsPageModelFactory;
            _playerListPageModelFactory = playerListPageModelFactory;
            _addPlayerPageModelFactory = addPlayerPageModelFactory;
            _addPlayerConfirmationPageModelFactory = addPlayerConfirmationPageModelFactory;
            _invitePlayerPageModelFactory = invitePlayerPageModelFactory;
            _invitePlayerConfirmationPageModelFactory = invitePlayerConfirmationPageModelFactory;
            _playerRepository = playerRepository;
            _userRepository = userRepository;
            _cashgameRepository = cashgameRepository;
            _authentication = authentication;
            _homegameRepository = homegameRepository;
        }

        public PlayerListPageModel GetListModel(string slug)
        {
            var homegame = _homegameRepository.GetByName(slug);
            var isInManagerMode = _authentication.IsInRole(slug, Role.Manager);
            var players = _playerRepository.GetList(homegame).OrderBy(o => o.DisplayName).ToList();
            return _playerListPageModelFactory.Create(homegame, players, isInManagerMode);
        }

        public PlayerDetailsPageModel GetDetailsModel(string slug, string playerName)
        {
            var homegame = _homegameRepository.GetByName(slug);
            var player = _playerRepository.GetByName(homegame, playerName);
            var user = _userRepository.GetById(player.UserId);
            var cashgames = _cashgameRepository.GetPublished(homegame);
            var isManager = _authentication.IsInRole(slug, Role.Manager);
            var hasPlayed = _cashgameRepository.HasPlayed(player);
            return _playerDetailsPageModelFactory.Create(homegame, player, user, cashgames, isManager, hasPlayed);
        }

        public AddPlayerPageModel GetAddModel(string slug, AddPlayerPostModel postModel)
        {
            var homegame = _homegameRepository.GetByName(slug);
            return _addPlayerPageModelFactory.Create(homegame, postModel);
        }

        public AddPlayerConfirmationPageModel GetAddConfirmationModel(string slug)
        {
            var homegame = _homegameRepository.GetByName(slug);
            return _addPlayerConfirmationPageModelFactory.Create(homegame);
        }

        public InvitePlayerPageModel GetInviteModel(string slug, InvitePlayerPostModel postModel)
        {
            var homegame = _homegameRepository.GetByName(slug);
            return _invitePlayerPageModelFactory.Create(homegame, postModel);
        }

        public InvitePlayerConfirmationPageModel GetInviteConfirmationModel(string slug)
        {
            var homegame = _homegameRepository.GetByName(slug);
            return _invitePlayerConfirmationPageModelFactory.Create(homegame);
        }

    }
}