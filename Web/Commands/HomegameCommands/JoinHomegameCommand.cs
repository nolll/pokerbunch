using Application.Services.Interfaces;
using Core.Classes;
using Core.Repositories;
using Web.Models.HomegameModels.Join;

namespace Web.Commands.HomegameCommands
{
    public class JoinHomegameCommand : Command
    {
        private readonly IAuthentication _authentication;
        private readonly IPlayerRepository _playerRepository;
        private readonly IInvitationCodeCreator _invitationCodeCreator;
        private readonly Homegame _homegame;
        private readonly JoinHomegamePostModel _postModel;

        public JoinHomegameCommand(
            IAuthentication authentication,
            IPlayerRepository playerRepository,
            IInvitationCodeCreator invitationCodeCreator,
            Homegame homegame,
            JoinHomegamePostModel postModel)
        {
            _authentication = authentication;
            _playerRepository = playerRepository;
            _invitationCodeCreator = invitationCodeCreator;
            _homegame = homegame;
            _postModel = postModel;
        }

        public override bool Execute()
        {
            if (!IsValid(_postModel)) return false;
            var player = GetMatchedPlayer(_homegame, _postModel.Code);
            if (player != null && player.IsUser)
            {
                var user = _authentication.GetUser();
                _playerRepository.JoinHomegame(player, _homegame, user);
                return true;
            }
            AddError("That code didn't work. Please check for errors and try again");
            return false;
        }

        private Player GetMatchedPlayer(Homegame homegame, string postedCode)
        {
            var players = _playerRepository.GetList(homegame);
            foreach (var player in players)
            {
                var code = _invitationCodeCreator.GetCode(player);
                if (code == postedCode)
                {
                    return player;
                }
            }
            return null;
        }
    }
}