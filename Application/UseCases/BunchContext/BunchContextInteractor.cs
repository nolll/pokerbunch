using Application.Services;
using Application.UseCases.AppContext;
using Core.Entities;
using Core.Repositories;

namespace Application.UseCases.BunchContext
{
    public class BunchContextInteractor : IBunchContextInteractor
    {
        private readonly IAppContextInteractor _appContextInteractor;
        private readonly IBunchRepository _bunchRepository;
        private readonly IAuth _auth;

        public BunchContextInteractor(
            IAppContextInteractor appContextInteractor,
            IBunchRepository bunchRepository,
            IAuth auth)
        {
            _appContextInteractor = appContextInteractor;
            _bunchRepository = bunchRepository;
            _auth = auth;
        }

        public BunchContextResult Execute(BunchContextRequest request)
        {
            var appContextResult = _appContextInteractor.Execute();

            var homegame = GetBunch(request);
            
            if(homegame == null)
                return new BunchContextResult(appContextResult);
            return new BunchContextResult(appContextResult, homegame.Slug, homegame.Id, homegame.DisplayName);
        }

        private Bunch GetBunch(BunchContextRequest request)
        {
            if (request.HasSlug)
                return _bunchRepository.GetBySlug(request.Slug);
            var bunches = _bunchRepository.GetByUser(_auth.CurrentUser);
            return bunches.Count == 1 ? bunches[0] : null;
        }
    }
}