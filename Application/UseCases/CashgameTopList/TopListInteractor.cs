using System.Linq;
using Core.Repositories;
using Core.Services.Interfaces;

namespace Application.UseCases.CashgameTopList
{
    public static class TopListInteractor
    {
        public static TopListResult Execute(IBunchRepository bunchRepository, ICashgameService cashgameService, TopListRequest request)
        {
            var homegame = bunchRepository.GetBySlug(request.Slug);
            var suite = cashgameService.GetSuite(homegame, request.Year);

            var sortedResults = suite.TotalResults.OrderByDescending(o => o.Winnings);
            var items = sortedResults.Select((o, index) => new TopListItem(o, index, homegame.Currency)).ToList();

            return new TopListResult(items, request.OrderBy, homegame.Slug, request.Year);
        }
    }
}