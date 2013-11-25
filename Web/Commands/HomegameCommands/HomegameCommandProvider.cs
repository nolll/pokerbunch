using Core.Classes;
using Core.Repositories;
using Core.Services;
using Web.ModelMappers;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Edit;
using Web.Models.HomegameModels.Join;

namespace Web.Commands.HomegameCommands
{
    public class HomegameCommandProvider : IHomegameCommandProvider
    {
        private readonly IHomegameModelMapper _homegameModelMapper;
        private readonly IHomegameRepository _homegameRepository;
        private readonly IUserContext _userContext;
        private readonly IPlayerRepository _playerRepository;
        private readonly ISlugGenerator _slugGenerator;
        private readonly IInvitationCodeCreator _invitationCodeCreator;

        public HomegameCommandProvider(
            IHomegameModelMapper homegameModelMapper,
            IHomegameRepository homegameRepository,
            IUserContext userContext,
            IPlayerRepository playerRepository,
            ISlugGenerator slugGenerator,
            IInvitationCodeCreator invitationCodeCreator)
        {
            _homegameModelMapper = homegameModelMapper;
            _homegameRepository = homegameRepository;
            _userContext = userContext;
            _playerRepository = playerRepository;
            _slugGenerator = slugGenerator;
            _invitationCodeCreator = invitationCodeCreator;
        }

        public Command GetAddCommand(AddHomegamePostModel postModel)
        {
            return new AddHomegameCommand(
                _homegameModelMapper,
                _homegameRepository,
                _userContext,
                _playerRepository,
                _slugGenerator,
                postModel);
        }

        public Command GetEditCommand(Homegame homegame, HomegameEditPostModel postModel)
        {
            return new EditHomegameCommand(
                _homegameModelMapper,
                _homegameRepository,
                homegame,
                postModel);
        }

        public Command GetJoinCommand(Homegame homegame, JoinHomegamePostModel postModel)
        {
            return new JoinHomegameCommand(
                _userContext,
                _playerRepository,
                _invitationCodeCreator,
                homegame,
                postModel);
        }
    }
}