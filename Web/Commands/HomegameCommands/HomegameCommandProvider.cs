using Application.Services;
using Core.Repositories;
using Web.ModelMappers;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Edit;
using Web.Models.HomegameModels.Join;
using Web.Security;

namespace Web.Commands.HomegameCommands
{
    public class HomegameCommandProvider : IHomegameCommandProvider
    {
        private readonly IHomegameModelMapper _homegameModelMapper;
        private readonly IHomegameRepository _homegameRepository;
        private readonly IAuth _auth;
        private readonly IPlayerRepository _playerRepository;
        private readonly ISlugGenerator _slugGenerator;
        private readonly IInvitationCodeCreator _invitationCodeCreator;

        public HomegameCommandProvider(
            IHomegameModelMapper homegameModelMapper,
            IHomegameRepository homegameRepository,
            IAuth auth,
            IPlayerRepository playerRepository,
            ISlugGenerator slugGenerator,
            IInvitationCodeCreator invitationCodeCreator)
        {
            _homegameModelMapper = homegameModelMapper;
            _homegameRepository = homegameRepository;
            _auth = auth;
            _playerRepository = playerRepository;
            _slugGenerator = slugGenerator;
            _invitationCodeCreator = invitationCodeCreator;
        }

        public Command GetAddCommand(AddHomegamePostModel postModel)
        {
            return new AddHomegameCommand(
                _homegameModelMapper,
                _homegameRepository,
                _auth,
                _playerRepository,
                _slugGenerator,
                postModel);
        }

        public Command GetEditCommand(string slug, HomegameEditPostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);

            return new EditHomegameCommand(
                _homegameModelMapper,
                _homegameRepository,
                homegame,
                postModel);
        }

        public Command GetJoinCommand(string slug, JoinHomegamePostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);

            return new JoinHomegameCommand(
                _auth,
                _playerRepository,
                _invitationCodeCreator,
                homegame,
                postModel);
        }
    }
}