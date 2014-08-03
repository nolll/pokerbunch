using System.Linq;
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

            var sortedResults = suite.TotalResults.OrderByDescending(o => o.Winnings);
            var items = sortedResults.Select((o, index) => new TopListItem(o, index, homegame.Currency)).ToList();

            return new TopListResult(items, request.OrderBy, homegame.Slug, request.Year);
        }
    }
}