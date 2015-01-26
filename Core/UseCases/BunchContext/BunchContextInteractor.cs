using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.UseCases.AppContext;

namespace Core.UseCases.BunchContext
{
    public static class BunchContextInteractor
    {
        public static BunchContextResult Execute(AppContextResult appContextResult, IBunchRepository bunchRepository, IAuth auth, BunchContextRequest request)
        {
            var bunch = GetBunch(bunchRepository, auth, request);

            if (bunch == null)
                return new BunchContextResult(appContextResult);
            return new BunchContextResult(appContextResult, bunch.Slug, bunch.Id, bunch.DisplayName);
        }

        private static Bunch GetBunch(IBunchRepository bunchRepository, IAuth auth, BunchContextRequest request)
        {
            if (request.HasSlug)
                return bunchRepository.GetBySlug(request.Slug);
            var bunches = bunchRepository.GetByUserId(auth.CurrentIdentity.UserId);
            return bunches.Count == 1 ? bunches[0] : null;
        }
    }
}