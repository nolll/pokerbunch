using Application.UseCases.CashgameContext;
using Application.UseCases.CashgameFacts;
using Web.Models.CashgameModels.Facts;

namespace Web.ModelFactories.CashgameModelFactories.Facts
{
    public class CashgameFactsPageBuilder : ICashgameFactsPageBuilder
    {
        private readonly ICashgameContextInteractor _contextInteractor;
        private readonly ICashgameFactsInteractor _cashgameFactsInteractor;

        public CashgameFactsPageBuilder(
            ICashgameContextInteractor contextInteractor,
            ICashgameFactsInteractor cashgameFactsInteractor)
        {
            _contextInteractor = contextInteractor;
            _cashgameFactsInteractor = cashgameFactsInteractor;
        }

        public CashgameFactsPageModel Build(string slug, int? year = null)
        {
            var contextResult = _contextInteractor.Execute(GetCashgameContextRequest(slug, year));
            var factsResult = _cashgameFactsInteractor.Execute(GetFactsRequest(slug, year));

            return new CashgameFactsPageModel(
                contextResult,
                factsResult);
        }

        private static CashgameFactsRequest GetFactsRequest(string slug, int? year)
        {
            return new CashgameFactsRequest
                {
                    Slug = slug,
                    Year = year
                };
        }

        private static CashgameContextRequest GetCashgameContextRequest(string slug, int? year)
        {
            return new CashgameContextRequest
                {
                    Slug = slug,
                    Year = year
                };
        }
    }
}