using Core.Entities;
using Core.Repositories;

namespace Web.Commands.PlayerCommands
{
    public class DeletePlayerCommand : Command
    {
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly Bunch _bunch;
        private readonly Player _player;

        public DeletePlayerCommand(
            ICashgameRepository cashgameRepository,
            IPlayerRepository playerRepository,
            Bunch bunch,
            Player player)
        {
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
            _bunch = bunch;
            _player = player;
        }

        public override bool Execute()
        {
            var hasPlayed = _cashgameRepository.HasPlayed(_player.Id);
            if (!hasPlayed)
            {
                _playerRepository.Delete(_bunch, _player);
                return true;
            }
            AddError("This player has played games, and can't be deleted");
            return false;
        }
    }
}