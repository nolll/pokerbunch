using Core.Classes;
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
        private readonly ICashgameRepository _cashgameRepository;

        public PlayerCommandProvider(
            IInvitationSender invitationSender,
            IPlayerRepository playerRepository,
            ICashgameRepository cashgameRepository)
        {
            _invitationSender = invitationSender;
            _playerRepository = playerRepository;
            _cashgameRepository = cashgameRepository;
        }

        public Command GetInviteCommand(Homegame homegame, Player player, InvitePlayerPostModel model)
        {
            return new InvitePlayerCommand(_invitationSender, homegame, player, model);
        }

        public Command GetAddCommand(Homegame homegame, AddPlayerPostModel model)
        {
            return new AddPlayerCommand(_playerRepository, homegame, model);
        }

        public Command GetDeleteCommand(Homegame homegame, Player player)
        {
            return new DeletePlayerCommand(_cashgameRepository, _playerRepository, homegame, player);
        }
    }
}