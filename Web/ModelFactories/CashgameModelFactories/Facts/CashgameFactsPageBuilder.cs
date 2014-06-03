using Application.UseCases.CashgameContext;
using Application.UseCases.CashgameFacts;
using Web.Models.CashgameModels.Facts;

namespace Web.ModelFactories.CashgameModelFactories.Facts
{
    public class CashgameFactsPageBuilder : ICashgameFactsPageBuilder
    {
        private readonly ICashgameContextInteractor _cashgameContextInteractor;
        private readonly ICashgameFactsInteractor _cashgameFactsInteractor;

        public CashgameFactsPageBuilder(
            ICashgameContextInteractor cashgameContextInteractor,
            ICashgameFactsInteractor cashgameFactsInteractor)
        {
            _cashgameContextInteractor = cashgameContextInteractor;
            _cashgameFactsInteractor = cashgameFactsInteractor;
        }

        public CashgameFactsPageModel Build(string slug, int? year = null)
        {
            var cashgameContextResult = _cashgameContextInteractor.Execute(GetCashgameContextRequest(slug, year));
            var factsResult = _cashgameFactsInteractor.Execute(GetFactsRequest(slug, year));

            return new CashgameFactsPageModel(
                cashgameContextResult,
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