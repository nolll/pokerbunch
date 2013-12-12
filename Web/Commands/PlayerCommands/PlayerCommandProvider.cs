using Core.Repositories;
using Core.Services;
using Web.Models.PlayerModels.Add;
using Web.Models.PlayerModels.Invite;

namespace Web.Commands.PlayerCommands
{
    public class PlayerCommandProvider : IPlayerCommandProvider
    {
        private readonly IInvitationSender _invitationSender;
        private readonly IPlayerRepository _playerRepository;
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public PlayerCommandProvider(
            IInvitationSender invitationSender,
            IPlayerRepository playerRepository,
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository)
        {
            _invitationSender = invitationSender;
            _playerRepository = playerRepository;
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
        }

        public Command GetInviteCommand(string slug, string playerName, InvitePlayerPostModel model)
        {
            var homegame = _homegameRepository.GetByName(slug);
            var player = _playerRepository.GetByName(homegame, playerName);
            return new InvitePlayerCommand(_invitationSender, homegame, player, model);
        }

        public Command GetAddCommand(string slug, AddPlayerPostModel model)
        {
            var homegame = _homegameRepository.GetByName(slug);
            return new AddPlayerCommand(_playerRepository, homegame, model);
        }

        public Command GetDeleteCommand(string slug, string playerName)
        {
            var homegame = _homegameRepository.GetByName(slug);
            var player = _playerRepository.GetByName(homegame, playerName);
            return new DeletePlayerCommand(_cashgameRepository, _playerRepository, homegame, player);
        }
    }
}