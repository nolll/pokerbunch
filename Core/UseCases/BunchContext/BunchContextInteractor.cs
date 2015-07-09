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

        public BunchContextInteractor(IUserRepository userRepository, IBunchRepository bunchRepository)
        {
            _userRepository = userRepository;
            _bunchRepository = bunchRepository;
        }

        public BunchContextResult Execute(BunchContextRequest request)
        {
            var appContextResult = new AppContextInteractor(_userRepository).Execute(new AppContextRequest(request.UserName));

            var bunch = GetBunch(appContextResult, request);
            if(bunch == null)
                return new BunchContextResult(appContextResult);
            return new BunchContextResult(appContextResult, bunch.Slug, bunch.Id, bunch.DisplayName);
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