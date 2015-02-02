using System.Collections.Generic;
using System.Linq;
using Core.Repositories;
using Core.Urls;

namespace Core.UseCases.CashgameHome
{
    public class CashgameHomeInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public CashgameHomeInteractor(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public CashgameHomeResult Execute(CashgameHomeRequest request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var years = _cashgameRepository.GetYears(bunch.Id);
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