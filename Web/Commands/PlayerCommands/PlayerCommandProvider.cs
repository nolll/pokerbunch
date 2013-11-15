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

        public PlayerCommandProvider(
            IInvitationSender invitationSender,
            IPlayerRepository playerRepository)
        {
            _invitationSender = invitationSender;
            _playerRepository = playerRepository;
        }

        public Command GetInviteCommand(Homegame homegame, Player player, InvitePlayerPostModel model)
        {
            return new InvitePlayerCommand(_invitationSender, homegame, player, model);
        }

        public Command GetAddCommand(Homegame homegame, AddPlayerPostModel model)
        {
            return new AddPlayerCommand(_playerRepository, homegame, model);
        }
    }
}