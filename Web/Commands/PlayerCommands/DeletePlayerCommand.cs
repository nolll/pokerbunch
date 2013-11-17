using Core.Classes;
using Core.Repositories;

namespace Web.Commands.PlayerCommands
{
    public class DeletePlayerCommand : Command
    {
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly Homegame _homegame;
        private readonly Player _player;

        public DeletePlayerCommand(
            ICashgameRepository cashgameRepository,
            IPlayerRepository playerRepository,
            Homegame homegame,
            Player player)
        {
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
            _homegame = homegame;
            _player = player;
        }

        public override bool Execute()
        {
            var hasPlayed = _cashgameRepository.HasPlayed(_player);
            if (!hasPlayed)
            {
                _playerRepository.DeletePlayer(_homegame, _player);
                return true;
            }
            AddError("This player has played games, and can't be deleted");
            return false;
        }
    }
}