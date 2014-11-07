using Core.Entities;
using Core.Repositories;

namespace Core.Services
{
    public class AccessHandler
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;

        public AccessHandler(IBunchRepository bunchRepository, IUserRepository userRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _userRepository = userRepository;
            _playerRepository = playerRepository;
        }

        public bool HasAccess(string slug, string userName, Role role)
        {
            var user = _userRepository.GetByNameOrEmail(userName);
            if (user.IsAdmin)
                return true;
            var bunch = _bunchRepository.GetBySlug(slug);
            var player = _playerRepository.GetByUserId(bunch.Id, user.Id);
            if (player == null)
                return false;
            if (player.IsInRole(role))
                return true;
            return false;
        }
    }
}