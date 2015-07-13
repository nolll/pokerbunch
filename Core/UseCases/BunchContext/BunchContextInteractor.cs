using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.UseCases.AppContext;

namespace Core.UseCases.BunchContext
{
    public class BunchContextInteractor
    {
        private readonly IUserRepository _userRepository;
        private readonly IBunchRepository _bunchRepository;
        private readonly IPlayerRepository _playerRepository;

        public BunchContextInteractor(IUserRepository userRepository, IBunchRepository bunchRepository, IPlayerRepository playerRepository)
        {
            _userRepository = userRepository;
            _bunchRepository = bunchRepository;
            _playerRepository = playerRepository;
        }

        public BunchContextResult Execute(BunchContextRequest request)
        {
            var appContext = new AppContextInteractor(_userRepository).Execute(new AppContextRequest(request.UserName));

            var bunch = GetBunch(appContext, request);
            if(bunch == null)
                return new BunchContextResult(appContext);

            var player = _playerRepository.GetByUserId(bunch.Id, appContext.UserId);
            var role = appContext.IsAdmin ? Role.Admin : player.Role;

            return new BunchContextResult(appContext, bunch.Slug, bunch.Id, bunch.DisplayName, role, player.Id);
        }

        private Bunch GetBunch(AppContextResult appContext, BunchContextRequest request)
        {
            if (!appContext.IsLoggedIn)
                return null;

            if (request.HasSlug)
            {
                try
                {
                    return _bunchRepository.GetBySlug(request.Slug);
                }
                catch (BunchNotFoundException)
                {
                    return null;
                }
            }
            var bunches = _bunchRepository.GetByUserId(appContext.UserId);
            return bunches.Count == 1 ? bunches[0] : null;
        }
    }
}