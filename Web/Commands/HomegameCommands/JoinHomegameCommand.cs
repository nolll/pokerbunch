using Core.Classes;
using Core.Repositories;
using Core.Services;
using Web.Models.HomegameModels.Join;

namespace Web.Commands.HomegameCommands
{
    public class JoinHomegameCommand : Command
    {
        private readonly IUserContext _userContext;
        private readonly IPlayerRepository _playerRepository;
        private readonly IInvitationCodeCreator _invitationCodeCreator;
        private readonly Homegame _homegame;
        private readonly JoinHomegamePostModel _postModel;

        public JoinHomegameCommand(
            IUserContext userContext,
            IPlayerRepository playerRepository,
            IInvitationCodeCreator invitationCodeCreator,
            Homegame homegame,
            JoinHomegamePostModel postModel)
        {
            _userContext = userContext;
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
                var user = _userContext.GetUser();
                _playerRepository.JoinHomegame(player, _homegame, user);
                return true;
            }
            AddError("That code didn't work. Please check for errors and try again");
            return false;
        }

        private Player GetMatchedPlayer(Homegame homegame, string postedCode)
        {
            var players = _playerRepository.GetAll(homegame);
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