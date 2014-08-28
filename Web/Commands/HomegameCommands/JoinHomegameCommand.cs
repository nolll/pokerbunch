using Application.Services;
using Core.Entities;
using Core.Repositories;
using Web.Models.HomegameModels.Join;

namespace Web.Commands.HomegameCommands
{
    public class JoinHomegameCommand : Command
    {
        private readonly IAuth _auth;
        private readonly IPlayerRepository _playerRepository;
        private readonly Bunch _bunch;
        private readonly JoinHomegamePostModel _postModel;

        public JoinHomegameCommand(
            IAuth auth,
            IPlayerRepository playerRepository,
            Bunch bunch,
            JoinHomegamePostModel postModel)
        {
            _auth = auth;
            _playerRepository = playerRepository;
            _bunch = bunch;
            _postModel = postModel;
        }

        public override bool Execute()
        {
            if (!IsValid(_postModel)) return false;
            var player = GetMatchedPlayer(_bunch, _postModel.Code);
            if (player != null && player.IsUser)
            {
                var user = _auth.CurrentUser;
                _playerRepository.JoinHomegame(player, _bunch, user);
                return true;
            }
            AddError("That code didn't work. Please check for errors and try again");
            return false;
        }

        private Player GetMatchedPlayer(Bunch bunch, string postedCode)
        {
            var players = _playerRepository.GetList(bunch);
            foreach (var player in players)
            {
                var code = InvitationCodeCreator.GetCode(player);
                if (code == postedCode)
                {
                    return player;
                }
            }
            return null;
        }
    }
}