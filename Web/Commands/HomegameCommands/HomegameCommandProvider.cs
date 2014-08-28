using Application.Services;
using Core.Repositories;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Edit;
using Web.Models.HomegameModels.Join;

namespace Web.Commands.HomegameCommands
{
    public class HomegameCommandProvider : IHomegameCommandProvider
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IAuth _auth;
        private readonly IPlayerRepository _playerRepository;

        public HomegameCommandProvider(
            IBunchRepository bunchRepository,
            IAuth auth,
            IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _auth = auth;
            _playerRepository = playerRepository;
        }

        public Command GetAddCommand(AddBunchPostModel postModel)
        {
            return new AddHomegameCommand(
                _bunchRepository,
                _auth,
                _playerRepository,
                postModel);
        }

        public Command GetEditCommand(string slug, HomegameEditPostModel postModel)
        {
            var homegame = _bunchRepository.GetBySlug(slug);

            return new EditHomegameCommand(
                _bunchRepository,
                homegame,
                postModel);
        }

        public Command GetJoinCommand(string slug, JoinHomegamePostModel postModel)
        {
            var homegame = _bunchRepository.GetBySlug(slug);

            return new JoinHomegameCommand(
                _auth,
                _playerRepository,
                homegame,
                postModel);
        }
    }
}