using Application.Services;
using Core.Entities;
using Web.Models.PlayerModels.Invite;

namespace Web.Commands.PlayerCommands
{
    public class InvitePlayerCommand : Command
    {
        private readonly IInvitationSender _invitationSender;
        private readonly Bunch _bunch;
        private readonly Player _player;
        private readonly InvitePlayerPostModel _model;

        public InvitePlayerCommand(IInvitationSender invitationSender, Bunch bunch, Player player, InvitePlayerPostModel model)
        {
            _invitationSender = invitationSender;
            _bunch = bunch;
            _player = player;
            _model = model;
        }

        public override bool Execute()
        {
            if (!IsValid(_model)) return false;
            _invitationSender.Send(_bunch, _player, _model.Email);
            return true;
        }
    }
}