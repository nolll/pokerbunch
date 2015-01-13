using System.Collections.Generic;
using System.Linq;
using Core.Repositories;
using Core.Urls;

namespace Core.UseCases.CashgameHome
{
    public static class CashgameHomeInteractor
    {
        public static CashgameHomeResult Execute(CashgameHomeRequest request, IBunchRepository bunchRepository, ICashgameRepository cashgameRepository)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var years = cashgameRepository.GetYears(bunch.Id);
            var startUrl = GetStartUrl(bunch.Slug, years);

            return new CashgameHomeResult(startUrl);
        }

        private static Url GetStartUrl(string slug, IList<int> years)
        {
            if (years.Count == 0)
                return new AddCashgameUrl(slug);
            return new CashgameMatrixUrl(slug, years.OrderBy(o => o).Last());
        }
    }
}