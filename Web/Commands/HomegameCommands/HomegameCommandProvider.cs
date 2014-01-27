using Application.Services.Interfaces;
using Core.Repositories;
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
        private readonly IAuthentication _authentication;
        private readonly IPlayerRepository _playerRepository;
        private readonly ISlugGenerator _slugGenerator;
        private readonly IInvitationCodeCreator _invitationCodeCreator;

        public HomegameCommandProvider(
            IHomegameModelMapper homegameModelMapper,
            IHomegameRepository homegameRepository,
            IAuthentication authentication,
            IPlayerRepository playerRepository,
            ISlugGenerator slugGenerator,
            IInvitationCodeCreator invitationCodeCreator)
        {
            _homegameModelMapper = homegameModelMapper;
            _homegameRepository = homegameRepository;
            _authentication = authentication;
            _playerRepository = playerRepository;
            _slugGenerator = slugGenerator;
            _invitationCodeCreator = invitationCodeCreator;
        }

        public Command GetAddCommand(AddHomegamePostModel postModel)
        {
            return new AddHomegameCommand(
                _homegameModelMapper,
                _homegameRepository,
                _authentication,
                _playerRepository,
                _slugGenerator,
                postModel);
        }

        public Command GetEditCommand(string slug, HomegameEditPostModel postModel)
        {
            var homegame = _homegameRepository.GetByName(slug);

            return new EditHomegameCommand(
                _homegameModelMapper,
                _homegameRepository,
                homegame,
                postModel);
        }

        public Command GetJoinCommand(string slug, JoinHomegamePostModel postModel)
        {
            var homegame = _homegameRepository.GetByName(slug);

            return new JoinHomegameCommand(
                _authentication,
                _playerRepository,
                _invitationCodeCreator,
                homegame,
                postModel);
        }
    }
}