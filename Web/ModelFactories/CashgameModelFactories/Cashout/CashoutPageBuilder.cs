using Application.UseCases.BunchContext;
using Web.Models.CashgameModels.Cashout;

namespace Web.ModelFactories.CashgameModelFactories.Cashout
{
    public class CashoutPageBuilder : ICashoutPageBuilder
    {
        private readonly IBunchContextInteractor _contextInteractor;

        public CashoutPageBuilder(
            IBunchContextInteractor contextInteractor)
        {
            _contextInteractor = contextInteractor;
        }

        public CashoutPageModel Build(string slug, CashoutPostModel postModel)
        {
            var contextResult = _contextInteractor.Execute(new BunchContextRequest(slug));

            return new CashoutPageModel(contextResult, postModel);
        }
    }
}