using Application.UseCases.BunchContext;
using Application.UseCases.CashgameOptions;
using Web.Models.CashgameModels.Add;

namespace Web.ModelFactories.CashgameModelFactories.Add
{
    public class AddCashgamePageBuilder : IAddCashgamePageBuilder
    {
        private readonly IBunchContextInteractor _contextInteractor;
        private readonly ICashgameOptionsInteractor _cashgameOptionsInteractor;

        public AddCashgamePageBuilder(
            IBunchContextInteractor contextInteractor,
            ICashgameOptionsInteractor cashgameOptionsInteractor)
        {
            _contextInteractor = contextInteractor;
            _cashgameOptionsInteractor = cashgameOptionsInteractor;
        }

        public AddCashgamePageModel Build(string slug, AddCashgamePostModel postModel)
        {
            var contextResult = _contextInteractor.Execute(new BunchContextRequest(slug));
            var optionsResult = _cashgameOptionsInteractor.Execute(new CashgameOptionsRequest(slug));

            return new AddCashgamePageModel(contextResult, optionsResult, postModel);
        } 
    }
}