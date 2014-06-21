using Core.Repositories;
using Core.Services.Interfaces;

namespace Application.UseCases.CashgameTopList
{
    public class TopListInteractor : ITopListInteractor
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameService _cashgameService;

        public TopListInteractor(
            IHomegameRepository homegameRepository,
            ICashgameService cashgameService)
        {
            _homegameRepository = homegameRepository;
            _cashgameService = cashgameService;
        }

        public TopListResult Execute(TopListRequest request)
        {
            var homegame = _homegameRepository.GetBySlug(request.Slug);
            var suite = _cashgameService.GetSuite(homegame, request.Year);

            return new TopListResult(homegame, suite.TotalResults, request.OrderBy, request.Year);
        }
    }
}