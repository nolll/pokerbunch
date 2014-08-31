using Core.Repositories;
using Web.Models.PlayerModels.Add;

namespace Web.Commands.PlayerCommands
{
    public class PlayerCommandProvider : IPlayerCommandProvider
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public PlayerCommandProvider(
            IPlayerRepository playerRepository,
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository)
        {
            _playerRepository = playerRepository;
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public Command GetAddCommand(string slug, AddPlayerPostModel model)
        {
            var homegame = _bunchRepository.GetBySlug(slug);
            return new AddPlayerCommand(_playerRepository, homegame, model);
        }

        public Command GetDeleteCommand(string slug, int playerId)
        {
            var homegame = _bunchRepository.GetBySlug(slug);
            var player = _playerRepository.GetById(playerId);
            return new DeletePlayerCommand(_cashgameRepository, _playerRepository, homegame, player);
        }
    }
}