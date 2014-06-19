using Application.Services;
using Application.UseCases.ApplicationContext;
using Core.Entities;
using Core.Repositories;

namespace Application.UseCases.BunchContext
{
    public class BunchContextInteractor : IBunchContextInteractor
    {
        private readonly IAppContextInteractor _appContextInteractor;
        private readonly IHomegameRepository _homegameRepository;
        private readonly IAuth _auth;

        public BunchContextInteractor(
            IAppContextInteractor appContextInteractor,
            IHomegameRepository homegameRepository,
            IAuth auth)
        {
            _appContextInteractor = appContextInteractor;
            _homegameRepository = homegameRepository;
            _auth = auth;
        }

        public BunchContextResult Execute(BunchContextRequest request)
        {
            var applicationContextResult = _appContextInteractor.Execute();

            var homegame = GetBunch(request);
            
            return new BunchContextResult(
                applicationContextResult,
                homegame.Slug,
                homegame.Id,
                homegame.DisplayName);
        }

        private Homegame GetBunch(BunchContextRequest request)
        {
            if (request.HasSlug)
                return _homegameRepository.GetBySlug(request.Slug);
            var bunches = _homegameRepository.GetByUser(_auth.CurrentUser);
            return bunches.Count == 1 ? bunches[0] : null;
        }
    }
}