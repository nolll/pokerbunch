using Application.UseCases.ApplicationContext;
using Core.Repositories;

namespace Application.UseCases.BunchContext
{
    public class BunchContextInteractor : IBunchContextInteractor
    {
        private readonly IApplicationContextInteractor _applicationContextInteractor;
        private readonly IHomegameRepository _homegameRepository;

        public BunchContextInteractor(
            IApplicationContextInteractor applicationContextInteractor,
            IHomegameRepository homegameRepository)
        {
            _applicationContextInteractor = applicationContextInteractor;
            _homegameRepository = homegameRepository;
        }

        public BunchContextResult Execute(BunchContextRequest request)
        {
            var applicationContextResult = _applicationContextInteractor.Execute();
            var homegame = _homegameRepository.GetBySlug(request.Slug);

            return new BunchContextResult(
                applicationContextResult,
                request.Slug,
                homegame.Id,
                homegame.DisplayName);
        }
    }
}