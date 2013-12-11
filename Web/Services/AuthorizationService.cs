using Core.Repositories;

namespace Core.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly IUserContext _userContext;

        public AuthorizationService(
            IHomegameRepository homegameRepository,
            IUserContext userContext)
        {
            _homegameRepository = homegameRepository;
            _userContext = userContext;
        }

        public bool IsPlayer(string gameName)
        {
            var homegame = _homegameRepository.GetByName(gameName);
            return _userContext.IsPlayer(homegame);
        }
    }
}
