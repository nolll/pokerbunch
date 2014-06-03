using Application.UseCases.ApplicationContext;
using Application.UseCases.CashgameContext;
using Application.UseCases.CashgameFacts;
using Web.Models.CashgameModels.Facts;

namespace Web.ModelFactories.CashgameModelFactories.Facts
{
    public class CashgameFactsPageBuilder : ICashgameFactsPageBuilder
    {
        private readonly ICashgameContextInteractor _cashgameContextInteractor;
        private readonly IApplicationContextInteractor _applicationContextInteractor;
        private readonly ICashgameFactsInteractor _cashgameFactsInteractor;

        public CashgameFactsPageBuilder(
            ICashgameContextInteractor cashgameContextInteractor,
            IApplicationContextInteractor applicationContextInteractor,
            ICashgameFactsInteractor cashgameFactsInteractor)
        {
            _cashgameContextInteractor = cashgameContextInteractor;
            _applicationContextInteractor = applicationContextInteractor;
            _cashgameFactsInteractor = cashgameFactsInteractor;
        }

        public CashgameFactsPageModel Build(string slug, int? year = null)
        {
            var applicationContextResult = _applicationContextInteractor.Execute();
            var cashgameContextResult = _cashgameContextInteractor.Execute(GetCashgameContextRequest(slug, year));
            var factsResult = _cashgameFactsInteractor.Execute(GetFactsRequest(slug, year));

            return new CashgameFactsPageModel(
                applicationContextResult,
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