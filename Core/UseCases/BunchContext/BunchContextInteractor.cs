using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.UseCases.AppContext;

namespace Core.UseCases.BunchContext
{
    public class BunchContextInteractor
    {
        private readonly IAuth _auth;
        private readonly IBunchRepository _bunchRepository;

        public BunchContextInteractor(IAuth auth, IBunchRepository bunchRepository)
        {
            _auth = auth;
            _bunchRepository = bunchRepository;
        }

        public BunchContextResult Execute(BunchContextRequest request)
        {
            var bunch = GetBunch(request);
            var appContextResult = new AppContextInteractor(_auth).Execute();

            if (bunch == null)
                return new BunchContextResult(appContextResult);
            return new BunchContextResult(appContextResult, bunch.Slug, bunch.Id, bunch.DisplayName);
        }

        private Bunch GetBunch(BunchContextRequest request)
        {
            if (request.HasSlug)
                return _bunchRepository.GetBySlug(request.Slug);
            var bunches = _bunchRepository.GetByUserId(_auth.CurrentIdentity.UserId);
            return bunches.Count == 1 ? bunches[0] : null;
        }
    }
}