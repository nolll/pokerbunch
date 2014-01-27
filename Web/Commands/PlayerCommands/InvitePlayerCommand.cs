using Application.Services.Interfaces;
using Core.Classes;
using Web.Models.PlayerModels.Invite;

namespace Web.Commands.PlayerCommands
{
    public class InvitePlayerCommand : Command
    {
        private readonly IInvitationSender _invitationSender;
        private readonly Homegame _homegame;
        private readonly Player _player;
        private readonly InvitePlayerPostModel _model;

        public InvitePlayerCommand(IInvitationSender invitationSender, Homegame homegame, Player player, InvitePlayerPostModel model)
        {
            _invitationSender = invitationSender;
            _homegame = homegame;
            _player = player;
            _model = model;
        }

        public override bool Execute()
        {
            if (!IsValid(_model)) return false;
            _invitationSender.Send(_homegame, _player, _model.Email);
            return true;
        }
    }
}