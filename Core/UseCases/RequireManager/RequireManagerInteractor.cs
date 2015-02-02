using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases.RequireManager
{
    public class RequireManagerInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;

        public RequireManagerInteractor(
            IBunchRepository bunchRepository,
            IUserRepository userRepository,
            IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _userRepository = userRepository;
            _playerRepository = playerRepository;
        }

        public void Execute(RequireManagerRequest request)
        {
            var accessHandler = new AccessHandler(_bunchRepository, _userRepository, _playerRepository);
            if (!accessHandler.HasAccess(request.Slug, request.UserName, Role.Player))
                throw new AccessDeniedException();
        }
    }
}