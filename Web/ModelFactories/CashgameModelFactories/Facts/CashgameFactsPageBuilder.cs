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
            var contextResult = _contextInteractor.Execute(new CashgameContextRequest(slug, year));
            var factsResult = _cashgameFactsInteractor.Execute(new CashgameFactsRequest(slug, year));

            return new CashgameFactsPageModel(contextResult, factsResult);
        }
    }
}