using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.UseCases.BunchDetails;

namespace Core.UseCases
{
    public abstract class BaseInteractor
    {
        private readonly AccessHandler _accessHandler;

        protected BaseInteractor(IBunchRepository bunchRepository, IUserRepository userRepository, IPlayerRepository playerRepository)
        {
            _accessHandler = new AccessHandler(bunchRepository, userRepository, playerRepository);
        }

        protected bool HasPlayerAccess(string slug, string userName)
        {
            return _accessHandler.HasAccess(slug, userName, Role.Player);
        }
    }
}