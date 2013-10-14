using Core.Classes;
using Core.Repositories;
using Web.ModelFactories.PlayerModelFactories;
using Web.Models.PlayerModels.Details;

namespace Web.ModelServices
{
    public class PlayerModelService : IPlayerModelService
    {
        private readonly IPlayerDetailsPageModelFactory _playerDetailsPageModelFactory;
        private readonly IPlayerRepository _playerRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IUserContext _userContext;

        public PlayerModelService(
            IPlayerDetailsPageModelFactory playerDetailsPageModelFactory,
            IPlayerRepository playerRepository,
            IUserRepository userRepository,
            ICashgameRepository cashgameRepository,
            IUserContext userContext)
        {
            _playerDetailsPageModelFactory = playerDetailsPageModelFactory;
            _playerRepository = playerRepository;
            _userRepository = userRepository;
            _cashgameRepository = cashgameRepository;
            _userContext = userContext;
        }

        public PlayerDetailsPageModel GetDetailsModel(User currentUser, Homegame homegame, string playerName)
        {
            var player = _playerRepository.GetByName(homegame, playerName);
            var user = _userRepository.GetUserByName(player.UserName);
            var cashgames = _cashgameRepository.GetPublished(homegame);
            var isManager = _userContext.IsInRole(homegame, Role.Manager);
            var hasPlayed = _cashgameRepository.HasPlayed(player);
            var runningGame = _cashgameRepository.GetRunning(homegame);
            return _playerDetailsPageModelFactory.Create(currentUser, homegame, player, user, cashgames, isManager, hasPlayed, runningGame);
        }
    }
}