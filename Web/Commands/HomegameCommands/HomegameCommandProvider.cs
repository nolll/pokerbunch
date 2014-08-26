using Application.Services;
using Core.Repositories;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Edit;
using Web.Models.HomegameModels.Join;

namespace Web.Commands.HomegameCommands
{
    public class HomegameCommandProvider : IHomegameCommandProvider
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly IAuth _auth;
        private readonly IPlayerRepository _playerRepository;

        public HomegameCommandProvider(
            IHomegameRepository homegameRepository,
            IAuth auth,
            IPlayerRepository playerRepository)
        {
            _homegameRepository = homegameRepository;
            _auth = auth;
            _playerRepository = playerRepository;
        }

        public Command GetAddCommand(AddHomegamePostModel postModel)
        {
            return new AddHomegameCommand(
                _homegameRepository,
                _auth,
                _playerRepository,
                postModel);
        }

        public Command GetEditCommand(string slug, HomegameEditPostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);

            return new EditHomegameCommand(
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
                homegame,
                postModel);
        }
    }
}