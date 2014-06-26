using Application.UseCases.BunchContext;
using Application.UseCases.CashgameDetails;
using Web.Models.CashgameModels.Details;

namespace Web.ModelFactories.CashgameModelFactories.Details
{
    public class CashgameDetailsPageBuilder : ICashgameDetailsPageBuilder
    {
        private readonly IBunchContextInteractor _contextInteractor;
        private readonly ICashgameDetailsInteractor _cashgameDetailsInteractor;

        public CashgameDetailsPageBuilder(
            IBunchContextInteractor contextInteractor,
            ICashgameDetailsInteractor cashgameDetailsInteractor)
        {
            _contextInteractor = contextInteractor;
            _cashgameDetailsInteractor = cashgameDetailsInteractor;
        }

        public CashgameDetailsPageModel Build(string slug, string dateStr)
        {
            var contextResult = _contextInteractor.Execute(new BunchContextRequest(slug));
            var cashgameDetailsResult = _cashgameDetailsInteractor.Execute(new CashgameDetailsRequest(slug, dateStr));

            return new CashgameDetailsPageModel(contextResult, cashgameDetailsResult);
        }
    }
}