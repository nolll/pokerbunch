using Application.Exceptions;
using Core.Classes;
using Core.Repositories;

namespace Application.Services
{
    public class Authorization : IAuthorization
    {
        private readonly IAuthentication _authentication;
        private readonly IHomegameRepository _homegameRepository;
        private readonly IPlayerRepository _playerRepository;

        public Authorization(
            IAuthentication authentication,
            IHomegameRepository homegameRepository,
            IPlayerRepository playerRepository)
        {
            _authentication = authentication;
            _homegameRepository = homegameRepository;
            _playerRepository = playerRepository;
        }

        public Role GetRole(Homegame homegame)
        {
            return _homegameRepository.GetHomegameRole(homegame, _authentication.GetUser());
        }

        public bool IsInRole(Homegame homegame, Role roleToCheck)
        {
            if (_authentication.IsAdmin())
            {
                return true;
            }
            var role = GetRole(homegame);
            return (int)role >= (int)roleToCheck;
        }

        private void RequireRole(Homegame homegame, Role role)
        {
            if (!IsInRole(homegame, role))
            {
                throw new AccessDeniedException();
            }
        }

        public void RequirePlayer(string bunchName)
        {
            var homegame = _homegameRepository.GetByName(bunchName);
            RequireRole(homegame, Role.Player);
        }

        public void RequireManager(string bunchName)
        {
            var homegame = _homegameRepository.GetByName(bunchName);
            RequireRole(homegame, Role.Manager);
        }

        public bool CanActAsPlayer(string slug, string playerName)
        {
            var homegame = _homegameRepository.GetByName(slug);
            var player = _playerRepository.GetByName(homegame, playerName);
            var currentUser = _authentication.GetUser();
            return _authentication.IsAdmin() || player.UserId == currentUser.Id;
        }

    }
}
