using Core.Repositories;
using Core.Services;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Edit;
using Web.Models.HomegameModels.Join;

namespace Web.Commands.HomegameCommands
{
    public class BunchCommandProvider : IBunchCommandProvider
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IAuth _auth;
        private readonly IPlayerRepository _playerRepository;

        public BunchCommandProvider(
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
            return new AddBunchCommand(
                _bunchRepository,
                _auth,
                _playerRepository,
                postModel);
        }

        public Command GetEditCommand(string slug, EditBunchPostModel postModel)
        {
            var bunch = _bunchRepository.GetBySlug(slug);

            return new EditBunchCommand(
                _bunchRepository,
                bunch,
                postModel);
        }

        public Command GetJoinCommand(string slug, JoinBunchPostModel postModel)
        {
            var bunch = _bunchRepository.GetBySlug(slug);

            return new JoinBunchCommand(
                _auth,
                _playerRepository,
                bunch,
                postModel);
        }
    }
}