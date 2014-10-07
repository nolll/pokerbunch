using System;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.UseCases.AppContext;

namespace Core.UseCases.BunchContext
{
    public class BunchContextInteractor
    {
        public static BunchContextResult Execute(Func<AppContextResult> appContext, IBunchRepository bunchRepository, IAuth auth, BunchContextRequest request)
        {
            var appContextResult = appContext();

            var bunch = GetBunch(bunchRepository, auth, request);

            if (bunch == null)
                return new BunchContextResult(appContextResult);
            return new BunchContextResult(appContextResult, bunch.Slug, bunch.Id, bunch.DisplayName);
        }

        private static Bunch GetBunch(IBunchRepository bunchRepository, IAuth auth, BunchContextRequest request)
        {
            if (request.HasSlug)
                return bunchRepository.GetBySlug(request.Slug);
            var bunches = bunchRepository.GetByUser(auth.CurrentUser);
            return bunches.Count == 1 ? bunches[0] : null;
        }
    }
}