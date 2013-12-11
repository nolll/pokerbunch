using Core.Classes;
using Core.Exceptions;
using Core.Repositories;

namespace Core.Services
{
    public class Authorization : IAuthorization
    {
        private readonly IAuthentication _authentication;
        private readonly IHomegameRepository _homegameRepository;

        public Authorization(
            IAuthentication authentication,
            IHomegameRepository homegameRepository)
        {
            _authentication = authentication;
            _homegameRepository = homegameRepository;
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
    }
}
